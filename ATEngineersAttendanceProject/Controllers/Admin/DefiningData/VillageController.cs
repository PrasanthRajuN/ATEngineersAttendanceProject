using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class VillageController : Controller
    {

        private readonly AppDbContext _context;
        public VillageController(AppDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Village()
        {
            var villages = await _context.Village.ToListAsync();
            return View("~/Views/Admin/DefiningData/Village.cshtml", villages);
        }
        //Add a village
        [HttpPost]
        public async Task<IActionResult> AddVillage(VillageModel village)
        {
            var existingSection = await _context.Village.FindAsync(village.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/Admin/DefiningData/Village.cshtml", await _context.Village.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Village.Add(village);
                await _context.SaveChangesAsync();
                return RedirectToAction("Village");
            }
            return View("~/Views/Admin/DefiningData/Village.cshtml", await _context.Village.ToListAsync());
        }
        //Delete a village
        [HttpPost]
        public async Task<IActionResult> DeleteVillage(int id)
        {
            var village = await _context.Village.FindAsync(id);
            if (village != null)
            {
                _context.Village.Remove(village);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Village");

        }

    }
}
