using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using AutoMapper;
using Domain.DrivingPort.Commands;
using Domain.DrivingPort.Models;
using Domain.DrivingPort.Queries;
using Domain.Exceptions;
using Domain.Port.Driving;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Lesson;
using Web.Models.Shared;

namespace Web.Controllers;

[Route("[controller]/[action]")]
public class LessonController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public int UserId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public LessonController(ILogger<HomeController> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    private async Task<List<CheckboxViewModel>> GetListSubjects()
    {
        var query = await _mediator.Send(new GetAllSubjectsQuery());
        var subjects = query
            .Select(o => new CheckboxViewModel { Id = o.Key, LabelName = o.Value, IsChecked = false })
            .ToList();
        return subjects;
    }

    [HttpGet]
    public async Task<ActionResult> Index(int id)
    {
        LessonViewModel model = new();
        model.Cities = await _mediator.Send(new GetAllCitiesQuery());
        model.Subjects = await GetListSubjects();
        model.LessonDetails = await _mediator.Send(new GetLessonDetailQuery { UserId = UserId, LessonId = id });
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, LessonDto userEvent)
    {
        try
        {
            await _mediator.Send(new UpdateLessonTimeCommand
            {
                UpdatedBy = UserId,
                EventId = id,
                UserEvent = userEvent
            });
            return RedirectToAction(nameof(Edit));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _mediator.Send(new DeleteLessonTimeCommand { UpdatedBy = UserId, EventId = id });
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }


    [HttpGet]
    public async Task<IActionResult> List(GetLessonQuery filter)
    {
        try
        {
            var times = await _mediator.Send(filter);
            return Json(times);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CheckIn()
    {
        try
        {
            await _mediator.Send(new LessonCheckInCommand { UpdatedBy = UserId });
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Clone(int id)
    {
        try
        {
            await _mediator.Send(new LessonCloneCommand { CreatedBy = UserId, LessonId = id });
            //TODO: Redirect to lesson page
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }
}