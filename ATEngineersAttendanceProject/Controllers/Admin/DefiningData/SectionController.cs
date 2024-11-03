using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class SectionController : Controller
    {
        private readonly AppDbContext _context;

        public SectionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Sections()
        {
            var sections = await _context.Sections.ToListAsync();
            return View("~/Views/Admin/DefiningData/Sections.cshtml", sections);
        }
        //Add a Section
        [HttpPost]
        public async Task<IActionResult> AddSection(SectionModel section)
        {
            var existingSection = await _context.Sections.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/Admin/DefiningData/Sections.cshtml", await _context.Sections.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Sections.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction("Sections");
            }
            return View("~/Views/Admin/DefiningData/Sections.cshtml", await _context.Sections.ToListAsync());
        }
        //Delete a Section
        [HttpPost]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Sections");
        }

    }
}
