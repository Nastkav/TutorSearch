using System.Security.Claims;
using AutoMapper;
using Domain.Commands;
using Domain.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Lesson;
using Web.Models.Shared;

namespace Web.Controllers;

public class LessonOldController : Controller
{
    public LessonOldController(ILogger<HomeController> logger, IMapper mapper, IMediator mediator)
    {
    }
    //
    // private async Task<List<CheckboxViewModel>> GetListSubjects()
    // {
    //     var query = await _mediator.Send(new GetAllSubjectsQuery());
    //     var subjects = query
    //         .Select(o => new CheckboxViewModel { Id = o.Key, LabelName = o.Value, IsChecked = false })
    //         .ToList();
    //     return subjects;
    // }
    //
    // [HttpGet]
    // public async Task<ActionResult> Details(int id)
    // {
    //     LessonViewModel model = new();
    //     model.Cities = await _mediator.Send(new GetAllCitiesQuery());
    //     model.Subjects = await GetListSubjects();
    //     model.LessonDetails = await _mediator.Send(new GetLessonDetailQuery { UserId = UserId, LessonId = id });
    //     return View(model);
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult> Create(CreateLessonTimeCommand command)
    // {
    //     try
    //     {
    //         if (command.CreatedBy == 0)
    //             command.CreatedBy = UserId;
    //         else if (command.CreatedBy != UserId)
    //             throw new IncorrectUserId($"command.CreatedBy={command.CreatedBy},app.UserId={UserId}");
    //         var id = await _mediator.Send(command);
    //         return Json(id);
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message);
    //         return StatusCode(500);
    //     }
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult> Edit(int id, UpdateLessonTimeCommand command)
    // {
    //     try
    //     {
    //         command.UpdatedBy = UserId;
    //         command.EventId = id;
    //         await _mediator.Send(command);
    //         return RedirectToAction(nameof(Edit));
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message);
    //         return StatusCode(500);
    //     }
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult> Delete(int id)
    // {
    //     try
    //     {
    //         await _mediator.Send(new DeleteLessonTimeCommand { UpdatedBy = UserId, EventId = id });
    //         return Json(1);
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message);
    //         return StatusCode(500);
    //     }
    // }
    //
    //
    // [HttpGet]
    // public async Task<IActionResult> List(GetEventQuery filter)
    // {
    //     try
    //     {
    //         var times = await _mediator.Send(filter);
    //         return Json(times);
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message);
    //         return StatusCode(500);
    //     }
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> CheckIn()
    // {
    //     try
    //     {
    //         await _mediator.Send(new LessonCheckInCommand { UpdatedBy = UserId });
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message);
    //         return StatusCode(500);
    //     }
    // }
}