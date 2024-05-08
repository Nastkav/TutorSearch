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

    public LessonController(ILogger<HomeController> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> Index(GetLessonsQuery query)
    {
        var lessons = new ListLessonViewModel();
        lessons.Lessons = await _mediator.Send(query);
        lessons.IsTutor = await _mediator.Send(new UserIsTutorQuery() { UserId = UserId });
        lessons.UserId = UserId;
        return View(lessons);
    }

    public async Task<IActionResult> Create()
    {
        var model = new LessonViewModel();
        if (UserId == 0)
            return NotFound();

        // model.Lesson = new Lesson();
        // model.Subjects = await GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        // model.HisStudents = await GetSelectList(new GetTutorStudentsQuery());
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LessonViewModel model)
    {
        if (ModelState.IsValid)
            // _context.Add(lessonModel);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        // ViewData["RequestId"] = new SelectList(new List<int>(), "Id", "Comment", model.RequestId);
        // ViewData["SubjectId"] = new SelectList(new List<int>(), "Id", "Name", model.SubjectId);
        // ViewData["TutorProfileId"] =
        // new SelectList(new List<int>(), "Id", "Address", lessonModel.TutorId);
        return View(model);
    }

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
    public async Task<IActionResult> Details(int id, LessonViewModel model)
    {
        if (id != model.Lesson.Id) return NotFound();

        if (ModelState.IsValid) return RedirectToAction(nameof(Index)); //TODO: Save
        model.Subjects = await GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpPost]
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