using System.Security.Claims;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Commands;
using Domain.Queries;
using Domain.Exceptions;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Shared;
using Web.Models.TutorProfile;

namespace Web.Controllers;

[Route("[controller]/[action]")]
[Authorize]
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public ProfileController(ILogger<ProfileController> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<List<CheckboxViewModel>> GetListCheckbox(IRequest<Dictionary<int, string>> q)
    {
        var query = await _mediator.Send(q);
        var list = query
            .Select(o => new CheckboxViewModel { Id = o.Key, LabelName = o.Value, IsChecked = false })
            .ToList();
        return list;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<List<SelectListItem>> GetSelectList(string defaultText, IRequest<Dictionary<int, string>> q)
    {
        var query = await _mediator.Send(q);
        var list = query
            .Select(o => new SelectListItem { Value = o.Key.ToString(), Text = o.Value })
            .ToList();
        list.Insert(0, new SelectListItem(defaultText, "0"));
        return list;
    }

    [HttpGet]
    [Route("/[controller]")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchTutor(SearchVm model)
    {
        //Main Lists
        model.Subjects = await GetSelectList("Оберіть тематику", new GetAllSubjectsQuery());
        model.Cities = await GetSelectList("Оберіть населений пункт", new GetAllCitiesQuery());
        //Add Cards
        model.TutorCards = [];
        foreach (var dbTutor in await _mediator.Send(model.Filters))
        {
            var tutorVm = _mapper.Map<TutorProfileModel, TutorCardVm>(dbTutor);
            //TODO: tutorVm.City = dbTutor.City?.FullName() ?? "";
            model.TutorCards.Add(tutorVm);
        }

        //Result
        return View(model);
    }

    [HttpGet]
    [Route("/[controller]/[action]/{id?}")]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id = 0)
    {
        if (id == 0)
            id = IdentityId;
        if (IdentityId == 0)
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        var model = await GetUserModel(id);
        return View(model);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ProfileVm> GetUserModel(int userId)
    {
        ProfileVm model = new() { IdentityId = IdentityId };
        model.Cities = await GetSelectList("Оберіть населений пункт", new GetAllCitiesQuery());
        model.UserVm = await _mediator.Send(new GetUserProfileQuery { ProfileId = userId });
        if (model.UserVm.TutorProfileEnabled)
        {
            model.Subjects = await GetSelectList("Оберіть тематику", new GetAllSubjectsQuery());
            model.TutorVm = await _mediator.Send(new GetTutorProfileQuery { ProfileId = userId });
            model.CreateRequestCommand.Subjects = model.Subjects; //Необхідний для показу списку предметів
            model.CreateRequestCommand.TutorId = userId;
        }

        return model;
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var model = await GetUserModel(IdentityId);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CheckFavorite(CheckFavoriteCommand command)
    {
        try
        {
            if (command.UserId != IdentityId)
                throw new IncorrectUserId($"command.UserId={command.UserId},app.UserId={IdentityId}");
            var result = await _mediator.Send(command);
            return Json(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProfileVm model)
    {
        // Ігнорувати помилки форми якщо не вчитель
        if (!model.UserVm.TutorProfileEnabled)
            foreach (var state in ModelState)
                if (state.Key.StartsWith(nameof(model.TutorVm)))
                    state.Value.ValidationState = ModelValidationState.Skipped;

        if (ModelState.IsValid)
        {
            await _mediator.Send(new UpdateUserCommand { Profile = model.UserVm });

            if (model.UserVm.TutorProfileEnabled)
            {
                if (model.TutorVm == null)
                    model.TutorVm = await _mediator.Send(new GetTutorProfileQuery { ProfileId = model.UserVm.Id });
                if (model.TutorVm.Id == 0)
                    model.TutorVm.Id = model.UserVm.Id;
                await _mediator.Send(new UpdateTutorCommand { Profile = model.TutorVm });
                model.Subjects = await GetSelectList("Оберіть предмет", new GetAllCitiesQuery());
                var tt = 0; //TODO: Add subjects dict to save
            }
        }

        model.Cities = await GetSelectList("Оберіть населений пункт", new GetAllCitiesQuery());
        return View(model);
    }
}