using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Domain.Queries;
using Domain.Commands;
using Domain.Exceptions;
using Web.Models.LessonRequest;

namespace Web.Controllers;

[Route("[controller]/[action]")]
public class LessonRequestController : Controller
{
    private readonly ILogger<LessonRequestController> _logger;
    private readonly IMediator _mediator;
    public int UserId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public LessonRequestController(ILogger<LessonRequestController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/[controller]")]
    public async Task<IActionResult> Index(bool onlyActive = false)
    {
        var vm = new LessonRequestVm
        {
            MyRequests = await _mediator.Send(new GetUserRequestsQuery { UserId = UserId, IsTutor = false }),
            RequestsForMe = await _mediator.Send(new GetUserRequestsQuery { UserId = UserId, IsTutor = true })
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestCommand command)
    {
        if (command.CreatedId == 0)
            command.CreatedId = UserId;
        else if (command.CreatedId != UserId)
            throw new IncorrectUserId($"command.CreatedBy={command.CreatedId},app.UserId={UserId}");

        if (ModelState.IsValid)
        {
            var result = await _mediator.Send(command);
            return Json(result);
        }
        else
        {
            var errList = (from item in ModelState
                where item.Value.Errors.Any()
                select item.Value.Errors[0].ErrorMessage).ToList();
            return StatusCode(500, errList);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRequestCommand command)
    {
        if (command.UpdatedBy == 0)
            command.UpdatedBy = UserId;
        else if (command.UpdatedBy != UserId)
            throw new IncorrectUserId($"command.UpdatedBy={command.UpdatedBy},app.UserId={UserId}");
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}