using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStudyAPI.DBContext;
using WebStudyAPI.Model;

namespace WebStudyAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class YearGradationsController : Controller
    {
        private readonly FiendContext _context;

        public YearGradationsController(FiendContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetGradations")]
        public async Task<List<YearGradation>> Get()
        {
            return _context.YearGradations != null ?
                          await _context.YearGradations.ToListAsync() : null;


        }

        // GET: YearGradations
        public async Task<IActionResult> Index()
        {
              return _context.YearGradations != null ? 
                          View(await _context.YearGradations.ToListAsync()) :
                          Problem("Entity set 'FiendContext.YearGradations'  is null.");
        }

        // GET: YearGradations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.YearGradations == null)
            {
                return NotFound();
            }

            var yearGradation = await _context.YearGradations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yearGradation == null)
            {
                return NotFound();
            }

            return View(yearGradation);
        }

        // GET: YearGradations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YearGradations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,YearName")] YearGradation yearGradation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yearGradation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yearGradation);
        }

        // GET: YearGradations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.YearGradations == null)
            {
                return NotFound();
            }

            var yearGradation = await _context.YearGradations.FindAsync(id);
            if (yearGradation == null)
            {
                return NotFound();
            }
            return View(yearGradation);
        }

        // POST: YearGradations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,YearName")] YearGradation yearGradation)
        {
            if (id != yearGradation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yearGradation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YearGradationExists(yearGradation.Id))
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
            return View(yearGradation);
        }

        // GET: YearGradations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.YearGradations == null)
            {
                return NotFound();
            }

            var yearGradation = await _context.YearGradations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yearGradation == null)
            {
                return NotFound();
            }

            return View(yearGradation);
        }

        // POST: YearGradations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.YearGradations == null)
            {
                return Problem("Entity set 'FiendContext.YearGradations'  is null.");
            }
            var yearGradation = await _context.YearGradations.FindAsync(id);
            if (yearGradation != null)
            {
                _context.YearGradations.Remove(yearGradation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YearGradationExists(int id)
        {
          return (_context.YearGradations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
