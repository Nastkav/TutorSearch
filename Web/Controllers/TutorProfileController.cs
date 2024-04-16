using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using Domain.DrivingPort.Commands;
using Domain.DrivingPort.Models;
using Domain.DrivingPort.Queries;
using Domain.Exceptions;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Shared;
using Web.Models.TutorProfile;

namespace Web.Controllers;

[Route("[controller]/[action]")]
public class TutorProfileController : Controller
{
    private readonly ILogger<TutorProfileController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public int UserId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public TutorProfileController(ILogger<TutorProfileController> logger, IMapper mapper, IMediator mediator)
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


    [HttpGet, Route("/[controller]")]
    public async Task<ActionResult> SearchTutor(GetTutorsQuery filter)
    {
        SearchViewModel model = new();
        model.Subjects = await GetListSubjects();
        model.Cities = await _mediator.Send(new GetAllCitiesQuery());

        foreach (var tutorDto in await _mediator.Send(filter))
            model.TutorCards.Add(_mapper.Map<TutorCardViewModel>(tutorDto));

        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        DetailsViewModel model = new();
        model.Subjects = await GetListSubjects();
        var tutor = await _mediator.Send(new GetTutorProfileQuery { ProfileId = id });
        if (tutor.Enabled)
        {
            model.TutorCard.Enabled = true;
            _mapper.Map(tutor, model);
            foreach (var subject in tutor.Subjects)
                model.Subjects[subject.Key].IsChecked = true;
        }

        return View(model);
    }


    [HttpGet]
    public async Task<ActionResult> Edit()
    {
        DetailsViewModel model = new();
        model.Cities = await _mediator.Send(new GetAllCitiesQuery());
        model.Subjects = await GetListSubjects();
        var tutor = await _mediator.Send(new GetTutorProfileQuery { ProfileId = UserId });
        if (tutor.Enabled)
        {
            model.TutorCard.Enabled = true;
            _mapper.Map(tutor, model);
            foreach (var subject in tutor.Subjects)
                model.Subjects[subject.Key].IsChecked = true;
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(TutorEditDto tutorEdit)
    {
        try
        {
            await _mediator.Send(new EditTutorProfileCommand { TutorEdit = tutorEdit, UpdatedBy = UserId });
            return RedirectToAction(nameof(Edit));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return View();
        }
    }

    [HttpPost]
    public async Task<ActionResult> CheckFavorite(CheckFavoriteCommand command)
    {
        try
        {
            if (command.UserId != UserId)
                throw new IncorrectUserId($"command.UserId={command.UserId},app.UserId={UserId}");
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500);
        }
    }
}