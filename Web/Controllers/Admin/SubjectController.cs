using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers.Admin;

// [Authorize(Roles = "Administrator")]
public class SubjectController : Controller
{
    private readonly AppDbContext _context;

    public SubjectController(AppDbContext context) => _context = context;

    // GET: Subject
    public async Task<IActionResult> Index() => View(await _context.Subjects.ToListAsync());

    // GET: Subject/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var subjectModel = await _context.Subjects
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subjectModel == null) return NotFound();

        return View(subjectModel);
    }

    // GET: Subject/Create
    public IActionResult Create() => View();

    // POST: Subject/Create

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,Name")] SubjectModel subjectModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(subjectModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(subjectModel);
    }

    // GET: Subject/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var subjectModel = await _context.Subjects.FindAsync(id);
        if (subjectModel == null) return NotFound();
        return View(subjectModel);
    }

    // POST: Subject/Edit/5

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SubjectModel subjectModel)
    {
        if (id != subjectModel.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(subjectModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectModelExists(subjectModel.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(subjectModel);
    }

    // GET: Subject/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var subjectModel = await _context.Subjects
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subjectModel == null) return NotFound();

        return View(subjectModel);
    }

    // POST: Subject/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var subjectModel = await _context.Subjects.FindAsync(id);
        if (subjectModel != null) _context.Subjects.Remove(subjectModel);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SubjectModelExists(int id) => _context.Subjects.Any(e => e.Id == id);
}