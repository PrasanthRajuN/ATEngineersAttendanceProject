using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class MandalController : Controller
    {

        private readonly AppDbContext _context;
        public MandalController(AppDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Mandal()
        {
            var mandals = await _context.Mandal.ToListAsync();
            return View("~/Views/Admin/DefiningData/Mandal.cshtml", mandals);
        }
        //Add a Mandal
        [HttpPost]
        public async Task<IActionResult> AddMandal(MandalModel mandal)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.Mandal.FindAsync(mandal.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/Admin/DefiningData/Mandal.cshtml", await _context.Mandal.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Mandal.Add(mandal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Mandal");
            }
            return View("~/Views/Admin/DefiningData/Mandal.cshtml", await _context.Mandal.ToListAsync());
        }
        //Delete a Mandal
        [HttpPost]
        public async Task<IActionResult> DeleteMandal(int id)
        {
            var mandal = await _context.Mandal.FindAsync(id);
            if (mandal != null)
            {
                _context.Mandal.Remove(mandal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Mandal");

        }

    }
}
