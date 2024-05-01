using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Domain.Queries;
using Domain.Commands;
using Domain.Exceptions;

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

    [HttpGet, Route("/[controller]")]
    public async Task<IActionResult> Index(bool onlyActive = false)
    {
        var query = new GetUserRequestsQuery { UserId = UserId, OnlyActive = onlyActive };
        var result = await _mediator.Send(query);
        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateRequestCommand command)
    {
        try
        {
            if (command.CreatedBy == 0)
                command.CreatedBy = UserId;
            else if (command.CreatedBy != UserId)
                throw new IncorrectUserId($"command.UserId={command.CreatedBy},app.UserId={UserId}");

            var result = await _mediator.Send(command);
            return Json(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Update(UpdateRequestCommand command)
    {
        try
        {
            if (command.UpdatedBy == 0)
                command.UpdatedBy = UserId;
            else if (command.UpdatedBy != UserId)
                throw new IncorrectUserId($"command.UpdatedBy={command.UpdatedBy},app.UserId={UserId}");

            var result = await _mediator.Send(command);
            return Json(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }
}