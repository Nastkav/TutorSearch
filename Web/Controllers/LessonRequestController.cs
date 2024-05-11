using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Domain.Queries;
using Domain.Commands;
using Domain.Exceptions;
using Domain.Helpers;
using Web.Models.LessonRequest;

namespace Web.Controllers;

[Route("[controller]/[action]")]
public class LessonRequestController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public LessonRequestController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index(bool onlyActive = false)
    {
        var vm = new LessonRequestVm
        {
            MyRequests =
                await _mediator.Send(new GetUserRequestsQuery { UserId = IdentityId, IsTutor = false }),
            RequestsForMe = await _mediator.Send(new GetUserRequestsQuery
                { UserId = IdentityId, IsTutor = true })
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestCommand command)
    {
        try
        {
            if (command.CreatedId == 0)
                command.CreatedId = IdentityId;
            else if (command.CreatedId != IdentityId)
                throw new IncorrectUserId($"command.CreatedBy={command.CreatedId},app.UserId={IdentityId}");

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                return Json(result);
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }

        var errList = (from item in ModelState
            where item.Value.Errors.Any()
            select item.Value.Errors[0].ErrorMessage).ToList();
        return StatusCode(500, errList);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRequestCommand command)
    {
        if (command.UpdatedBy == 0)
            command.UpdatedBy = IdentityId;
        else if (command.UpdatedBy != IdentityId)
            throw new IncorrectUserId($"command.UpdatedBy={command.UpdatedBy},app.UserId={IdentityId}");
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}