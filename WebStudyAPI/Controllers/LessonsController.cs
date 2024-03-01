using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WebStudyAPI.DBContext;
using WebStudyAPI.Model;

namespace WebStudyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : Controller
    {
        private readonly FiendContext _context;

        public LessonsController(FiendContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetLessons")]
        public async Task<String> Get(string math)
        {

            /*var lessonsLinq = await _context.Lessons
                                            .Include(l=> l.Year_gradation_id)
                                            .Include(l=> l.Subject_id)
                                            .ToListAsync();*/

            var lessons = from Lesson in _context.Lessons
                          join YearGradation in _context.YearGradations on Lesson.Year_gradation_id equals YearGradation.Id
                          join Subject in _context.Subjects on Lesson.Subject_id equals Subject.Id
                          select new
                          {
                              id = Lesson.Id,
                              year_gradation = YearGradation.Year_name,
                              less_week = Lesson.Less_week,
                              subject = Subject.Subject_name,
                              lesson = Lesson.Lesson_name,
                              lessq = Lesson.Less_query,
                              urlq = Lesson.Url_query
                          };
            lessons = lessons.Where(l => l.subject == math).OrderBy(l => l.less_week);
            return _context.Lessons != null ?
                          /*await _context.Lessons.ToListAsync() */ lessons.ToJson() : null;


        }
        [HttpGet("math/lesson/{id:int}")]
        // GET: Lessons
        public async Task<String> Getbyid(int id)
        {
              return _context.Lessons != null ? 
                          _context.Lessons.Where(l => l.Id == id).ToJson():
                          null;
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,YearGradationID,LessonWeek,SubjectID,LessonName,Question,RightAnswer")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,YearGradationID,LessonWeek,SubjectID,LessonName,Question,RightAnswer")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
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
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'FiendContext.Lessons'  is null.");
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
          return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
