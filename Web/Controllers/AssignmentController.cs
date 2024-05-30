using System.Security.Claims;
using Domain.Commands;
using Domain.Helpers;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Helpers;
using Web.Models.Assignments;

namespace Web.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
[Produces("text/html; charset=utf-8")]
public class AssignmentController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public AssignmentController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public void AssignmentValidation(AssignmentVm model)
    {
        if (DateTime.Now > model.Assignment.Deadline)
            ModelState.AddModelError("Assignment.Deadline", "Не можна встановити термін здачі у минулому");
        if (model.Assignment.StudentsIds.Count == 0)
            ModelState.AddModelError("Assignment.StudentsIds", "Треба вибирати хоча б одного учня");
        if (model.Assignment.TutorId != IdentityId)
            ModelState.AddModelError("Assignment.TutorId", "Обрано невірного вчителя");
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetAssignmentsQuery? filter)
    {
        var model = new AssignmentListVm();
        if (filter == null)
            filter = new GetAssignmentsQuery();
        filter.UserId = IdentityId;

        model.Assignments = await _mediator.Send(filter);
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery());
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        _helper.UpdateSelf(model.HisStudents, IdentityId);
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new AssignmentVm();

        if (IdentityId == 0)
            return NotFound();
        //Отримання даних та наповнення моделі
        model.UserId = model.Assignment.TutorId = IdentityId;
        model.Subjects =
            await _helper.GetSelectList(new GetAllSubjectsQuery() { TutorId = IdentityId }, "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(AssignmentVm model)
    {
        AssignmentValidation(model);
        if (ModelState.IsValid)
        {
            if (model.Assignment.Description == null) // fix empty description
                model.Assignment.Description = "";

            var assignmentId = await _mediator.Send(new CreateAssignmentCommand()
            {
                CreatedId = IdentityId,
                Assignment = model.Assignment
            });
            return RedirectToAction(nameof(Edit), new { id = assignmentId });
        }

        model.UserId = model.Assignment.TutorId = IdentityId;
        model.Subjects =
            await _helper.GetSelectList(new GetAllSubjectsQuery() { TutorId = IdentityId }, "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        return View(model);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var model = new AssignmentVm();

        if (IdentityId == 0)
            return NotFound();

        var curAssignment = await _mediator.Send(new GetOneAssignmentQuery { AssignmentId = id, UserId = IdentityId });

        if (curAssignment == null)
            return NotFound();

        model.UserId = IdentityId;
        model.Assignment = curAssignment;
        model.Subjects =
            await _helper.GetSelectList(new GetAllSubjectsQuery() { TutorId = IdentityId }, "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        return View(model);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> Edit(int id, AssignmentVm model)
    {
        AssignmentValidation(model);
        if (id != model.Assignment.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _mediator.Send(new UpdateAssignmentCommand()
            {
                UpdatedBy = IdentityId,
                AssignmentId = model.Assignment.Id,
                Title = model.Assignment.Title,
                Description = model.Assignment.Description,
                Deadline = model.Assignment.Deadline,
                SubjectId = model.Assignment.SubjectId,
                StudentIds = model.Assignment.StudentsIds
            });
            return RedirectToAction(nameof(Index));
        }

        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        return View(model);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Response.StatusCode = 400;
        if (id != 0)
        {
            var success = await _mediator.Send(new DeleteAssignmentCommand()
            {
                AssignmentId = id,
                TutorId = IdentityId
            });
            if (success)
                return RedirectToAction(nameof(Index));
        }

        return NotFound();
    }
}