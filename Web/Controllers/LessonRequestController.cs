using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Domain.Queries;
using Domain.Commands;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter.Helpers;
using Microsoft.AspNetCore.Authorization;
using Web.Helpers;
using Web.Models.LessonRequest;

namespace Web.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
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
            if (!command.From.HasValue || !command.To.HasValue)
            {
                ModelState.AddModelError("From", "Треба обрати час");
            }
            else
            {
                if (command.From.Value < DateTime.Now.AddHours(-1))
                    ModelState.AddModelError("From", " Ви не можете створити подію в минулому");
                if (command.From.Value.Date != command.To.Value.Date)
                    ModelState.AddModelError("Lesson.To", "Зустріч має проходити протягом дня");
                if (command.From.Value.Date > command.To.Value.Date)
                    ModelState.AddModelError("Lesson.To", "Можливо ви переплутали час місцями");
            }

            if (command.CreatedId == 0)
                command.CreatedId = IdentityId;
            else if (command.CreatedId != IdentityId)
                throw new Exception($"command.CreatedBy={command.CreatedId},app.UserId={IdentityId}");

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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (IdentityId == 0)
            return NotFound();

        var requestsForMe = await _mediator.Send(new GetUserRequestsQuery { UserId = IdentityId, IsTutor = true });
        var model = requestsForMe.Where(x => x.Id == id).FirstOrDefault();
        if (model == null)
            return RedirectToAction(nameof(Index));
        else
            return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Update(LessonRequest model)
    {
        var command = new UpdateRequestCommand()
        {
            Id = model.Id,
            UpdatedBy = IdentityId,
            TutorId = model.TutorId,
            Status = (LessonRequestStatus)int.Parse(Request.Form["Status"][1]),
            TutorComment = model.TutorComment,
            From = model.From,
            To = model.To
        };
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}