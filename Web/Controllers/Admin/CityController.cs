using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers.Admin;

[Authorize(Roles = "Administrator")]
[Route("/[controller]/[action]")]
[Produces("text/html; charset=utf-8")]
public class CityController : Controller
{
    private readonly AppDbContext _context;

    public CityController(AppDbContext context) => _context = context;

    // GET: City
    [HttpGet]
    public async Task<IActionResult> Index() => View(await _context.Cities.ToListAsync());


    // GET: City/Create
    [HttpGet]
    public IActionResult Create() => View();

    // POST: City/Create
    [HttpPost]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Region,CreatedAt,UpdatedAt")]
        CityModel cityModel)
    {
        ModelState.Remove("Id");
        if (ModelState.IsValid)
        {
            _context.Add(cityModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(cityModel);
    }

    // GET: City/Edit/5
    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var cityModel = await _context.Cities.FindAsync(id);
        if (cityModel == null) return NotFound();
        return View(cityModel);
    }

    // POST: City/Edit/5
    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Region,CreatedAt,UpdatedAt")]
        CityModel cityModel)
    {
        if (id != cityModel.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cityModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityModelExists(cityModel.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(cityModel);
    }

    // GET: City/Delete/5
    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var cityModel = await _context.Cities
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cityModel == null) return NotFound();

        return View(cityModel);
    }

    // POST: City/Delete/5
    [HttpPost]
    [Route("{id}")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cityModel = await _context.Cities.FindAsync(id);
        if (cityModel != null) _context.Cities.Remove(cityModel);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CityModelExists(int id) => _context.Cities.Any(e => e.Id == id);
}