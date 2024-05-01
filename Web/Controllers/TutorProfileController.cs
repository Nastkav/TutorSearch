using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using Domain.Commands;
using Domain.Commands;
using Domain.Entities;
using Domain.Models;
using Domain.Queries;
using Domain.Exceptions;
using Domain.Queries;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    private async Task<List<CheckboxViewModel>> GetListCheckbox(IRequest<Dictionary<int, string>> q)
    {
        var query = await _mediator.Send(q);
        var subjects = query
            .Select(o => new CheckboxViewModel { Id = o.Key, LabelName = o.Value, IsChecked = false })
            .ToList();
        return subjects;
    }

    private async Task<List<SelectListItem>> GetSelectList(IRequest<Dictionary<int, string>> q)
    {
        var query = await _mediator.Send(q);
        var subjects = query
            .Select(o => new SelectListItem { Value = o.Key.ToString(), Text = o.Value })
            .ToList();
        return subjects;
    }

    [HttpGet, Route("/[controller]")]
    public async Task<ActionResult> SearchTutor(SearchViewModel model) //+
    {
        //Main Lists
        model.Subjects = await GetSelectList(new GetAllSubjectsQuery());
        model.Subjects.Insert(0, new SelectListItem("Оберіть тематику", "0"));
        model.Cities = await GetSelectList(new GetAllCitiesQuery());
        model.Cities.Insert(0, new SelectListItem("Оберіть населений пункт", "0"));
        //Add Cards
        model.TutorCards = [];
        foreach (var dbTutor in await _mediator.Send(model.Filters))
        {
            var tutorVm = _mapper.Map<TutorProfileModel, TutorCardViewModel>(dbTutor);
            tutorVm.City = dbTutor.City?.FullName() ?? "";
            model.TutorCards.Add(tutorVm);
        }

        //Result
        return View(model);
    }

    [HttpGet, Route("/[controller]/[action]/{id?}")]
    public async Task<ActionResult> Details(int id = 0)
    {
        if (id == 0)
            id = UserId;

        DetailsViewModel model = new() { CurrentUserId = UserId };
        model.Subjects = await GetListCheckbox(new GetAllSubjectsQuery());
        var tutor = await _mediator.Send(new GetTutorProfileQuery { ProfileId = id });

        _mapper.Map(tutor, model);
        _mapper.Map(tutor, model.TutorCard);
        //Set Enabled Subjects
        foreach (var subject in tutor.Subjects)
            model.Subjects[subject.Key].IsChecked = true;

        return View(model);
    }


    [HttpGet]
    public async Task<ActionResult> Edit()
    {
        DetailsViewModel model = new();
        model.Subjects = await GetListCheckbox(new GetAllSubjectsQuery());
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
    public async Task<ActionResult> Edit(DetailsViewModel model)
    {
        var tutorEdit = _mapper.Map<TutorEditDto>(model);
        _mapper.Map(model.TutorCard, tutorEdit);
        foreach (var checkbox in model.Subjects)
            if (checkbox.IsChecked)
                tutorEdit.Subjects[checkbox.Id] = checkbox.LabelName;
        await _mediator.Send(new EditTutorProfileCommand { TutorEdit = tutorEdit, RequestBy = UserId });
        return RedirectToAction(nameof(Edit));
    }

    [HttpPost]
    public async Task<ActionResult> CheckFavorite(CheckFavoriteCommand command)
    {
        try
        {
            if (command.UserId != UserId)
                throw new IncorrectUserId($"command.UserId={command.UserId},app.UserId={UserId}");
            var result = await _mediator.Send(command);
            return Json(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}