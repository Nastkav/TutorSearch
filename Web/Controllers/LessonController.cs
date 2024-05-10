using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Identity;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra.DatabaseAdapter.Models;
using MediatR;
using SQLitePCL;
using Web;
using Web.Models.Lesson;

namespace Web.Controllers;

[Route("[controller]/[action]")]
public class LessonController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public int UserId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<List<SelectListItem>> GetSelectList(IRequest<Dictionary<int, string>> q, string defaultText = "")
    {
        var query = await _mediator.Send(q);
        var list = query
            .Select(o => new SelectListItem { Value = o.Key.ToString(), Text = o.Value })
            .ToList();
        if (defaultText != "")
            list.Insert(0, new SelectListItem(defaultText, "0"));
        return list;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private void LessonValidation(LessonViewModel model)
    {
        if (model.Lesson.TutorId == model.UserId && model.UserId == UserId)
            ModelState.AddModelError("Lesson.TutorId", "Вибрано невірного вчителя");
        if (model.Lesson.From.Date != model.Lesson.To.Date)
            ModelState.AddModelError("Lesson.To", "Зустріч має проходити протягом дня");
        if (model.Lesson.From.Date > model.Lesson.To.Date)
            ModelState.AddModelError("Lesson.To", "Можливо ви переплутали час місцями");
        if (model.Lesson.From < DateTime.Today.AddDays(-7))
            ModelState.AddModelError("Lesson.To", "Занадто стара дата");
        if (model.Lesson.StudentsIds.Count == 0)
            ModelState.AddModelError("Lesson.Students", "Треба вибирати хоча б одного учня");
    }

    public LessonController(ILogger<HomeController> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var lessons = new ListLessonViewModel();
        lessons.Lessons = await _mediator.Send(new GetLessonsQuery() { UserId = UserId, TutorId = UserId });
        lessons.IsTutor = await _mediator.Send(new UserIsTutorQuery() { UserId = UserId });
        lessons.UserId = UserId;
        return View(lessons);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new LessonViewModel();

        if (UserId == 0)
            return NotFound();

        var userData = await _mediator.Send(new GetUserProfileQuery() { ProfileId = UserId });
        model.Lesson.TutorName = userData.FullName();
        model.Lesson.From = DateTime.Now;
        model.Lesson.To = DateTime.Now.AddHours(1);
        model.Lesson.TutorId = model.UserId = UserId;
        model.Subjects = await GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(LessonViewModel model)
    {
        LessonValidation(model);
        if (ModelState.IsValid)
        {
            if (model.Lesson.Comment == null)
                model.Lesson.Comment = "";
            await _mediator.Send(new CreateLessonCommand() { CreatedId = UserId, Lesson = model.Lesson });
            return RedirectToAction(nameof(Index));
        }

        model.Lesson.TutorId = model.UserId = UserId;
        model.Subjects = await GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var model = new LessonViewModel();

        if (UserId == 0)
            return NotFound();

        var curLesson = await _mediator.Send(new GetOneLessonQuery { LessonId = id, UserId = UserId });

        if (curLesson == null)
            return NotFound();

        model.UserId = UserId;
        model.Lesson = curLesson;
        if (model.Lesson.TutorId == UserId)
            model.IsTutor = true;
        model.Subjects = await GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> Details(int id, LessonViewModel model)
    {
        LessonValidation(model);
        if (id != model.Lesson.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _mediator.Send(new UpdateLessonTimeCommand()
            {
                UpdatedBy = UserId,
                From = model.Lesson.From,
                To = model.Lesson.To,
                Comment = model.Lesson.Comment,
                SubjectId = model.Lesson.SubjectId
            });
            return RedirectToAction(nameof(Index)); //TODO: Save Update Lesson
        }

        model.Subjects = await GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        Response.StatusCode = 400;
        if (id != 0)
        {
            var success = await _mediator.Send(new DeleteLessonCommand() { LessonId = id, TutorId = UserId });
            if (success)
                Response.StatusCode = 200;
        }

        return Content(id.ToString());
    }
}