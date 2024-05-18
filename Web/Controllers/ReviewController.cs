using System.Security.Claims;
using Domain.Commands;
using Domain.Helpers;
using Domain.Models;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web.Helpers;
using Web.Models.Assignments;
using Web.Models.Profile;
using ZstdSharp.Unsafe;

namespace Web.Controllers;

[Authorize]
public class ReviewController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }


    [HttpGet]
    // [Route("{tutorId}")]
    [Route("[controller]/[action]/{tutorId}")]
    public async Task<IActionResult> Edit(int tutorId)
    {
        if (IdentityId == 0)
            return NotFound();
        var userReview = await _mediator.Send(new GetReviewQuery { TutorId = tutorId, AuthorId = IdentityId });
        return View(userReview);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(Review model)
    {
        if (model.AuthorId != IdentityId)
            ModelState.AddModelError("AuthorId", "Автор вказаний невірно");
        if (model.Rating == 0) ModelState.AddModelError("Rating", "Оцінка обов'язкова");

        if (ModelState.IsValid)
            try
            {
                model = await _mediator.Send(new AddOrUpdateReviewCommand() { Review = model });
                return RedirectToRoute(new
                {
                    controller = "Profile",
                    action = "Details",
                    id = model.TutorId
                });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("AuthorId", e.Message);
            }

        return View(model);
    }
}