using System.Security.Claims;
using AutoMapper;
using Domain.Commands;
using Domain.Queries;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Lessons;
using Web.Models.Shared;

namespace Web.Controllers;

public class SessionController : Controller
{
    private readonly IMediator _mediator;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public SessionController(IMediator mediator) => _mediator = mediator;


    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreateSessionCommand command)
    {
        try
        {
            if (command.CreatedBy == 0)
                command.CreatedBy = IdentityId;
            else if (command.CreatedBy != IdentityId)
                throw new Exception($"command.CreatedBy={command.CreatedBy},app.UserId={IdentityId}");
            var id = await _mediator.Send(command);
            return Json(id);
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Edit(int id, UpdateSessionCommand command)
    {
        try
        {
            command.UpdatedBy = IdentityId;
            command.EventId = id;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Edit));
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(new DeleteSessionCommand() { UpdatedBy = IdentityId, EventId = id });
            return Json(1);
        }
        catch
        {
            return StatusCode(500);
        }
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> List(GetEventQuery filter)
    {
        try
        {
            var times = await _mediator.Send(filter);
            return Json(times);
        }
        catch
        {
            return StatusCode(500);
        }
    }
}