using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Commands;
using Domain.Helpers;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Web;
using Web.Models.Assignments;
using ZstdSharp.Unsafe;

namespace Web.Controllers;

[Authorize]
public class AssignmentController : Controller
{
    private readonly IMediator _mediator;
    private readonly ControllerHelpers _helper;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public AssignmentController(IMediator mediator)
    {
        _mediator = mediator;
        _helper = new ControllerHelpers(mediator);
    }

    public void AssignmentValidation(AssignmentVm model)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetAssignmentQuery? filter)
    {
        var model = new AssignmentListVm();
        if (filter == null)
            filter = new GetAssignmentQuery();
        filter.UserId = IdentityId;

        model.Assignments = await _mediator.Send(filter);
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new AssignmentVm();

        if (IdentityId == 0)
            return NotFound();
        //Отримання даних та наповнення моделі
        model.UserId = model.Assignment.TutorId = IdentityId;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(AssignmentVm model)
    {
        AssignmentValidation(model);
        if (ModelState.IsValid)
        {
            await _mediator.Send(new CreateAssignmentCommand()
            {
                CreatedId = IdentityId,
                Assignment = model.Assignment
            });
            return RedirectToAction(nameof(Index));
        }

        model.UserId = model.Assignment.TutorId = IdentityId;
        model.Subjects = await _helper.GetSelectList(new GetAllSubjectsQuery(), "Оберіть тематику");
        model.HisStudents = await _helper.GetSelectList(new GetTutorStudentsQuery() { TutorId = IdentityId });
        return View(model);
    }
}

// public async Task<IActionResult> Details(int? id)
// {
//     if (id == null) return NotFound();
//
//     var taskModel = await _context.Tasks
//         .Include(t => t.Tutor)
//         .FirstOrDefaultAsync(m => m.Id == id);
//     if (taskModel == null) return NotFound();
//
//     return View(taskModel);
// }
//
// public IActionResult Create() =>
//     // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address");
//     View();
//
// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> Create(
//     [Bind("Id,TutorId,Title,Description,Deadline,CreatedAt,UpdatedAt")]
//     TaskModel taskModel)
// {
//     if (ModelState.IsValid)
//     {
//         _context.Add(taskModel);
//         await _context.SaveChangesAsync();
//         return RedirectToAction(nameof(Index));
//     }
//
//     // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", taskModel.TutorId);
//     return View(taskModel);
// }
//
// // GET: Task/Edit/5
// public async Task<IActionResult> Edit(int? id)
// {
//     if (id == null) return NotFound();
//
//     var taskModel = await _context.Tasks.FindAsync(id);
//     if (taskModel == null) return NotFound();
//     // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", taskModel.TutorId);
//     return View(taskModel);
// }
//
// // POST: Task/Edit/5
//
// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> Edit(int id,
//     [Bind("Id,TutorId,Title,Description,Deadline,CreatedAt,UpdatedAt")]
//     TaskModel taskModel)
// {
//     if (id != taskModel.Id) return NotFound();
//
//     if (ModelState.IsValid)
//     {
//         try
//         {
//             _context.Update(taskModel);
//             await _context.SaveChangesAsync();
//         }
//         catch (DbUpdateConcurrencyException)
//         {
//             if (!TaskModelExists(taskModel.Id))
//                 return NotFound();
//             else
//                 throw;
//         }
//
//         return RedirectToAction(nameof(Index));
//     }
//
//     // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", taskModel.TutorId);
//     return View(taskModel);
// }