using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Domain.Exceptions;
using Domain.Port.Driving;
using Domain.Queries;
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
    public async Task<ActionResult> Details(int id)
    {
        LessonViewModel model = new();
        model.Cities = await _mediator.Send(new GetAllCitiesQuery());
        model.Subjects = await GetListSubjects();
        model.LessonDetails = await _mediator.Send(new GetLessonDetailQuery { UserId = UserId, LessonId = id });
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateLessonTimeCommand command)
    {
        if (command.CreatedBy == 0)
            command.CreatedBy = UserId;
        else if (command.CreatedBy != UserId)
            throw new IncorrectUserId($"command.CreatedBy={command.CreatedBy},app.UserId={UserId}");
        await _mediator.Send(command);
        return RedirectToAction(nameof(Edit));
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, UpdateLessonTimeCommand command)
    {
        try
        {
            command.UpdatedBy = UserId;
            command.EventId = id;
            await _mediator.Send(command);
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
    public async Task<IActionResult> Clone(int id) =>
        // try
        // {
        //     await _mediator.Send(new LessonCloneCommand { CreatedBy = UserId, LessonId = id });
        //     //TODO: Redirect to lesson page
        //     return RedirectToAction(nameof(Index));
        // }
        // catch (Exception e)
        // {
        //     _logger.LogError(e.Message);
        StatusCode(500);
    // }
}