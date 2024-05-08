using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra.DatabaseAdapter.Models;
using Web;

namespace Web.Controllers
{
    public class SolutionController : Controller
    {
        private readonly TemplateDbContext _context;

        public SolutionController(TemplateDbContext context)
        {
            _context = context;
        }

        // GET: Solution
        public async Task<IActionResult> Index()
        {
            var templateDbContext = _context.Solutions.Include(s => s.Student).Include(s => s.Task);
            return View(await templateDbContext.ToListAsync());
        }

        // GET: Solution/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solutionModel = await _context.Solutions
                .Include(s => s.Student)
                .Include(s => s.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solutionModel == null)
            {
                return NotFound();
            }

            return View(solutionModel);
        }

        // GET: Solution/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Avatar");
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description");
            return View();
        }

        // POST: Solution/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskId,StudentId,Answer,CreatedAt,UpdatedAt")] SolutionModel solutionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solutionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Avatar", solutionModel.StudentId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description", solutionModel.TaskId);
            return View(solutionModel);
        }

        // GET: Solution/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solutionModel = await _context.Solutions.FindAsync(id);
            if (solutionModel == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Avatar", solutionModel.StudentId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description", solutionModel.TaskId);
            return View(solutionModel);
        }

        // POST: Solution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskId,StudentId,Answer,CreatedAt,UpdatedAt")] SolutionModel solutionModel)
        {
            if (id != solutionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solutionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolutionModelExists(solutionModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Avatar", solutionModel.StudentId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Description", solutionModel.TaskId);
            return View(solutionModel);
        }

        // GET: Solution/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solutionModel = await _context.Solutions
                .Include(s => s.Student)
                .Include(s => s.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solutionModel == null)
            {
                return NotFound();
            }

            return View(solutionModel);
        }

        // POST: Solution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solutionModel = await _context.Solutions.FindAsync(id);
            if (solutionModel != null)
            {
                _context.Solutions.Remove(solutionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolutionModelExists(int id)
        {
            return _context.Solutions.Any(e => e.Id == id);
        }
    }
}
