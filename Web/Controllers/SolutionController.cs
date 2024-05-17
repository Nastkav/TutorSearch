using System.Security.Claims;
using Domain.Commands;
using Domain.Helpers;
using Domain.Queries;
using Infra.DatabaseAdapter.Helpers;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Helpers;
using Web.Models.Assignments;

namespace Web.Controllers;

[Authorize]
public class SolutionController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public SolutionController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetSolutionsQuery? filter)
    {
        var model = new SolutionListVm();
        if (filter == null) filter = new GetSolutionsQuery();

        filter.UserId = IdentityId;
        model.Solutions = await _mediator.Send(filter);
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery());
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        model.HisTutors = await _helper.GetSelectList(new GetStudentTutorsQuery() { StudentId = IdentityId });
        model.Assignments = await _helper.GetSelectList(new GetAssignmentNamesQuery()
        {
            TutorId = IdentityId,
            StudentId = IdentityId
        });
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = new SolutionVm();

        if (IdentityId == 0) return NotFound();

        var curSolution = await _mediator.Send(new GetOneSolutionQuery { SolutionId = id, UserId = IdentityId });

        if (curSolution == null) return NotFound();
        model.UserId = IdentityId;
        model.Solution = curSolution;
        model.IsTutor = IdentityId == model.Solution.TutorId;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, SolutionVm model)
    {
        if (model.Solution.SolutionFiles.Count > 5)
            ModelState.AddModelError("Solution.SolutionFiles", "До 5 файлів доступно для завантаження");

        if (id != model.Solution.Id) return NotFound();

        if (ModelState.IsValid)
        {
            //Оновити статус рішення якщо учень відправив відповідь
            if (!model.IsTutor && model.Solution.Answer != null && model.Solution.Answer.Length > 0)
                model.Solution.Status = SolutionStatus.Review;

            await _mediator.Send(new UpdateSolutionCommand()
            {
                UpdatedBy = IdentityId,
                SolutionId = model.Solution.Id,
                Status = model.Solution.Status,
                Answer = model.Solution.Answer,
                TutorComment = model.Solution.TutorComment
            });
            return RedirectToAction(nameof(Index));
        }

        model.IsTutor = IdentityId == model.Solution.TutorId;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        return View(model);
    }
}