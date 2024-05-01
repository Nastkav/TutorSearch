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

public class CourseController : Controller
{
    private readonly TemplateDbContext _context;

    public CourseController(TemplateDbContext context) => _context = context;

    // GET: Course
    public async Task<IActionResult> Index()
    {
        var templateDbContext =
            _context.Courses; //.Include(c => c.Request).Include(c => c.Subject).Include(c => c.Tutor);
        var ll = await templateDbContext.ToListAsync();
        return View(ll);
    }

    // GET: Course/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var courseModel = await _context.Courses
            .Include(c => c.Request)
            .Include(c => c.Subject)
            .Include(c => c.Tutor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (courseModel == null) return NotFound();

        return View(courseModel);
    }

    // GET: Course/Create
    public IActionResult Create()
    {
        ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Comment");
        ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
        ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address");
        return View();
    }

    // POST: Course/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,SubjectId,RequestId,TutorId")] CourseModel courseModel)
    {
        if (ModelState.IsValid)
        {
            courseModel.Id = Guid.NewGuid();
            _context.Add(courseModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Comment", courseModel.RequestId);
        ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", courseModel.SubjectId);
        ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", courseModel.TutorId);
        return View(courseModel);
    }

    // GET: Course/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var courseModel = await _context.Courses.FindAsync(id);
        if (courseModel == null) return NotFound();
        ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Comment", courseModel.RequestId);
        ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", courseModel.SubjectId);
        ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", courseModel.TutorId);
        return View(courseModel);
    }

    // POST: Course/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id,
        [Bind("Id,Title,SubjectId,RequestId,TutorId")]
        CourseModel courseModel)
    {
        if (id != courseModel.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(courseModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseModelExists(courseModel.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Comment", courseModel.RequestId);
        ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", courseModel.SubjectId);
        ViewData["TutorId"] = new SelectList(_context.TutorProfiles, "Id", "Address", courseModel.TutorId);
        return View(courseModel);
    }

    // GET: Course/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var courseModel = await _context.Courses
            // .Include(c => c.Request)
            // .Include(c => c.Subject)
            // .Include(c => c.Tutor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (courseModel == null) return NotFound();

        return View(courseModel);
    }

    // POST: Course/Delete/5
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var courseModel = await _context.Courses.FindAsync(id);
        if (courseModel != null) _context.Courses.Remove(courseModel);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CourseModelExists(Guid id) => _context.Courses.Any(e => e.Id == id);
}