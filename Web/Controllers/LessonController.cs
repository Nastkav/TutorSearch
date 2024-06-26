using System.Security.Claims;
using Domain.Commands;
using Domain.Helpers;
using Domain.Queries;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Helpers;
using Web.Models.Lessons;

namespace Web.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
public class LessonController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public LessonController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private void LessonValidation(LessonVm model)
    {
        if (model.Lesson.TutorId != IdentityId)
            ModelState.AddModelError("Lesson.TutorId", "Обрано невірного вчителя");
        if (model.Lesson.From.Date != model.Lesson.To.Date)
            ModelState.AddModelError("Lesson.To", "Зустріч має проходити протягом дня");
        if (model.Lesson.From.Date > model.Lesson.To.Date)
            ModelState.AddModelError("Lesson.To", "Можливо ви переплутали час місцями");
        if (model.Lesson.From < DateTime.Now.AddHours(-1))
            ModelState.AddModelError("Lesson.To", " Ви не можете створити подію в минулому");
        if (model.Lesson.StudentsIds.Count == 0)
            ModelState.AddModelError("Lesson.StudentsIds", "Треба вибирати хоча б одного учня");
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var lessons = new ListLessonVm();
        lessons.Lessons = await _mediator.Send(new GetLessonsQuery()
        {
            StudentId = IdentityId,
            TutorId = IdentityId
        });
        lessons.IsTutor = await _mediator.Send(new UserIsTutorQuery() { UserId = IdentityId });
        lessons.UserId = IdentityId;
        return View(lessons);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new LessonVm();

        if (IdentityId == 0)
            return NotFound();

        var userData = await _mediator.Send(new GetUserProfileQuery() { ProfileId = IdentityId });
        model.Lesson.TutorName = userData.FullName;
        model.Lesson.From = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
        model.Lesson.To = model.Lesson.From.AddHours(1);
        model.Lesson.TutorId = model.UserId = IdentityId;
        model.Subjects =
            await _helper.GetSelectList(new GetAllSubjectsQuery() { TutorId = IdentityId }, "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(LessonVm model)
    {
        LessonValidation(model);

        if (ModelState.IsValid)
        {
            if (model.Lesson.Comment == null)
                model.Lesson.Comment = "";
            await _mediator.Send(new CreateLessonCommand() { CreatedId = IdentityId, Lesson = model.Lesson });
            return RedirectToAction(nameof(Index));
        }

        model.Lesson.TutorId = model.UserId = IdentityId;
        model.Subjects =
            await _helper.GetSelectList(new GetAllSubjectsQuery() { TutorId = IdentityId }, "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = new LessonVm();

        if (IdentityId == 0)
            return NotFound();

        var curLesson = await _mediator.Send(new GetOneLessonQuery { LessonId = id, UserId = IdentityId });

        if (curLesson == null)
            return NotFound();

        model.UserId = IdentityId;
        model.Lesson = curLesson;
        if (model.Lesson.TutorId == IdentityId)
            model.IsTutor = true;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue)
            return NotFound();

        var tempLesson = await _mediator.Send(new GetOneLessonQuery { LessonId = id.Value, UserId = IdentityId });
        if (tempLesson == null)
            return NotFound();
        else
            return View(new LessonVm() { Lesson = tempLesson });
    }

    [HttpPost]
    public async Task<ActionResult> Delete(int id)
    {
        Response.StatusCode = 400;
        if (id != 0)
        {
            var success =
                await _mediator.Send(new DeleteLessonCommand() { LessonId = id, TutorId = IdentityId });
            if (success)
                return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}