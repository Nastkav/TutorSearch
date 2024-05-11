using System.Security.Claims;
using Domain.Commands;
using Domain.Helpers;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;
using Web.Models.Lessons;

namespace Web.Controllers;

[Route("[controller]/[action]")]
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
    private void LessonValidation(LessonViewModel model)
    {
        if (model.Lesson.TutorId == model.UserId && model.UserId == IdentityId)
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


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var lessons = new ListLessonViewModel();
        lessons.Lessons = await _mediator.Send(new GetLessonsQuery()
        {
            UserId = IdentityId,
            TutorId = IdentityId
        });
        lessons.IsTutor = await _mediator.Send(new UserIsTutorQuery() { UserId = IdentityId });
        lessons.UserId = IdentityId;
        return View(lessons);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new LessonViewModel();

        if (IdentityId == 0)
            return NotFound();

        var userData = await _mediator.Send(new GetUserProfileQuery() { ProfileId = IdentityId });
        model.Lesson.TutorName = userData.FullName;
        model.Lesson.From = DateTime.Now;
        model.Lesson.To = DateTime.Now.AddHours(1);
        model.Lesson.TutorId = model.UserId = IdentityId;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
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
            await _mediator.Send(new CreateLessonCommand() { CreatedId = IdentityId, Lesson = model.Lesson });
            return RedirectToAction(nameof(Index));
        }

        model.Lesson.TutorId = model.UserId = IdentityId;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var model = new LessonViewModel();

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

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> Details(int id, LessonViewModel model)
    {
        LessonValidation(model);
        if (id != model.Lesson.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _mediator.Send(new UpdateSessionCommand()
            {
                UpdatedBy = IdentityId,
                From = model.Lesson.From,
                To = model.Lesson.To,
                Comment = model.Lesson.Comment,
                SubjectId = model.Lesson.SubjectId
            });
            return RedirectToAction(nameof(Index)); //TODO: Save Update Lesson
        }

        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = model.Lesson.TutorId });
        return View(model);
    }


    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        Response.StatusCode = 400;
        if (id != 0)
        {
            var success =
                await _mediator.Send(new DeleteLessonCommand() { LessonId = id, TutorId = IdentityId });
            if (success)
                Response.StatusCode = 200;
        }

        return Content(id.ToString());
    }
}