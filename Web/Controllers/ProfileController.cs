using System.Security.Claims;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Commands;
using Domain.Queries;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Helpers;
using Web.Models.Shared;
using Web.Models.Profile;

namespace Web.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
public class ProfileController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }


    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpGet]
    [Route("/")]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index(SearchVm model)
    {
        //Main Lists
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.Cities = await _helper.GetSelectList(new GetAllCitiesQuery(), "Оберіть населений пункт");

        //Add Cards
        model.Filters.IdentityId = IdentityId;
        var userIds = await _mediator.Send(model.Filters);
        foreach (var userId in userIds)
        {
            var card = new TutorCardVm()
            {
                Subjects = model.Subjects,
                TutorVm = await _mediator.Send(new GetTutorProfileQuery { ProfileId = userId }),
                UserVm = await _mediator.Send(new GetUserProfileQuery { ProfileId = userId })
            };
            card.UserVm.CityId =
                model.Cities.FirstOrDefault(x => x.Value == card.UserVm.CityId)?.Text ?? "Не вказано";
            model.TutorCards.Add(card);
        }

        //Result
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("{id}")]
    public async Task<IActionResult> Details(int id = 0)
    {
        if (id == 0)
            id = IdentityId;

        var model = await GetUserModel(id, true);
        model.Reviews = await _mediator.Send(new GetTutorReviewsQuery() { TutorId = id });
        model.CreateRequestCommand.CreatedId = IdentityId;
        return View(model);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public async Task<ProfileVm> GetUserModel(int userId, bool onlyTutorData = false)
    {
        ProfileVm model = new() { IdentityId = IdentityId };
        model.Cities = await _helper.GetSelectList(new GetAllCitiesQuery(), "Оберіть населений пункт");
        model.UserVm = await _mediator.Send(new GetUserProfileQuery { ProfileId = userId });
        if (model.UserVm.ProfileEnabled)
        {
            var q = new GetAllSubjectsQuery();
            model.Subjects = await _helper.GetSelectList(q, "Оберіть тематику");
            model.TutorVm = await _mediator.Send(new GetTutorProfileQuery { ProfileId = userId });
            model.CreateRequestCommand.TutorId = userId;

            //Необхідний для показу списку предметів
            if (onlyTutorData)
                q.TutorId = userId;
            model.CreateRequestCommand.Subjects = await _helper.GetSelectList(q);
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
    public async Task<IActionResult> Edit(ProfileVm model)
    {
        // Ігнорувати помилки форми якщо не вчитель
        if (!model.UserVm.ProfileEnabled)
            foreach (var state in ModelState)
                if (state.Key.StartsWith(nameof(model.TutorVm)))
                    state.Value.ValidationState = ModelValidationState.Skipped;

        if (ModelState.IsValid)
        {
            await _mediator.Send(new UpdateUserCommand { Profile = model.UserVm });

            //Upload new avatar
            if (model.NewAvatar != null)
            {
                var imgBytes = await _helper.BytesFromFormFile(model.NewAvatar);
                await _mediator.Send(new SaveAvatarCommand() { UserId = IdentityId, ImageBytes = imgBytes });
            }

            if (model.UserVm.ProfileEnabled)
            {
                if (model.TutorVm == null)
                    model.TutorVm = await _mediator.Send(new GetTutorProfileQuery { ProfileId = model.UserVm.Id });
                if (model.TutorVm.Id == 0)
                    model.TutorVm.Id = model.UserVm.Id;
                await _mediator.Send(new UpdateTutorCommand { Profile = model.TutorVm });
                model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть предмет");
            }
        }

        model.Cities = await _helper.GetSelectList(new GetAllCitiesQuery(), "Оберіть населений пункт");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> IsFavorite(CheckFavoriteQuery query)
    {
        var isChecked = await _mediator.Send(query);
        return Json(new { status = isChecked });
    }

    [HttpPost]
    public async Task<IActionResult> IsFavorite(CheckFavoriteCommand command)
    {
        try
        {
            if (command.UserId != IdentityId)
                throw new Exception($"command.UserId={command.UserId},app.UserId={IdentityId}");
            var isChecked = await _mediator.Send(command);
            return Json(new { status = isChecked });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}