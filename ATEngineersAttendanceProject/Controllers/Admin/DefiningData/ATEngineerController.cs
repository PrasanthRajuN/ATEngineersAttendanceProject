using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.ATEngineers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class ATEngineerController : Controller
    {
        private readonly AppDbContext _context;

        public ATEngineerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ATEngineer()
        {
            var sections = await _context.ATEngineer.ToListAsync();
            return View("~/Views/Admin/DefiningData/ATEngineer.cshtml", sections);
        }
        //Add a ATEngineer
        [HttpPost]
        public async Task<IActionResult> AddATEngineer(ATEngineerModel section)
        {
            var existingSection = await _context.ATEngineer.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/Admin/DefiningData/ATEngineer.cshtml", await _context.ATEngineer.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.ATEngineer.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction("ATEngineer");
            }

            return View("~/Views/Admin/DefiningData/ATEngineer.cshtml", await _context.ATEngineer.ToListAsync());
        }

        // Delete a ATEngineer
        [HttpPost]
        public async Task<IActionResult> DeleteATEngineer(int id)
        {
            var section = await _context.ATEngineer.FindAsync(id);
            if (section != null)
            {
                _context.ATEngineer.Remove(section);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ATEngineer");
        }

    }
}
