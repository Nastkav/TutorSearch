using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra.DatabaseAdapter.Models;
using Web;

namespace Web.Controllers;

public class TaskController : Controller
{
    private readonly TemplateDbContext _context;

    public TaskController(TemplateDbContext context) => _context = context;

    // GET: Task
    public async Task<IActionResult> Index()
    {
        var templateDbContext = _context.Tasks.Include(t => t.Tutor);
        return View(await templateDbContext.ToListAsync());
    }

    // GET: Task/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var taskModel = await _context.Tasks
            .Include(t => t.Tutor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskModel == null) return NotFound();

        return View(taskModel);
    }

    // GET: Task/Create
    public IActionResult Create() =>
        // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address");
        View();

    // POST: Task/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,TutorId,Title,Description,Deadline,CreatedAt,UpdatedAt")] TaskModel taskModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(taskModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", taskModel.TutorId);
        return View(taskModel);
    }

    // GET: Task/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var taskModel = await _context.Tasks.FindAsync(id);
        if (taskModel == null) return NotFound();
        // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", taskModel.TutorId);
        return View(taskModel);
    }

    // POST: Task/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,TutorId,Title,Description,Deadline,CreatedAt,UpdatedAt")] TaskModel taskModel)
    {
        if (id != taskModel.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(taskModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskModelExists(taskModel.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", taskModel.TutorId);
        return View(taskModel);
    }

    // GET: Task/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var taskModel = await _context.Tasks
            .Include(t => t.Tutor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskModel == null) return NotFound();

        return View(taskModel);
    }

    // POST: Task/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var taskModel = await _context.Tasks.FindAsync(id);
        if (taskModel != null) _context.Tasks.Remove(taskModel);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaskModelExists(int id) => _context.Tasks.Any(e => e.Id == id);
}