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
    public class ProfileController : Controller
    {
        private readonly TemplateDbContext _context;

        public ProfileController(TemplateDbContext context)
        {
            _context = context;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var templateDbContext = _context.TutorProfiles.Include(t => t.City);
            return View(await templateDbContext.ToListAsync());
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorProfileModel = await _context.TutorProfiles
                .Include(t => t.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutorProfileModel == null)
            {
                return NotFound();
            }

            return View(tutorProfileModel);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImgPath,Enabled,Address,Experience,Descriptions,CityId,HourRate,OnlineAccess,TutorHomeAccess,StudentHomeAccess,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] TutorProfileModel tutorProfileModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorProfileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", tutorProfileModel.CityId);
            return View(tutorProfileModel);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorProfileModel = await _context.TutorProfiles.FindAsync(id);
            if (tutorProfileModel == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", tutorProfileModel.CityId);
            return View(tutorProfileModel);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImgPath,Enabled,Address,Experience,Descriptions,CityId,HourRate,OnlineAccess,TutorHomeAccess,StudentHomeAccess,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] TutorProfileModel tutorProfileModel)
        {
            if (id != tutorProfileModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorProfileModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorProfileModelExists(tutorProfileModel.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", tutorProfileModel.CityId);
            return View(tutorProfileModel);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorProfileModel = await _context.TutorProfiles
                .Include(t => t.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutorProfileModel == null)
            {
                return NotFound();
            }

            return View(tutorProfileModel);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutorProfileModel = await _context.TutorProfiles.FindAsync(id);
            if (tutorProfileModel != null)
            {
                _context.TutorProfiles.Remove(tutorProfileModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorProfileModelExists(int id)
        {
            return _context.TutorProfiles.Any(e => e.Id == id);
        }
    }
}
