using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class DistrictController : Controller
    {

        private readonly AppDbContext _context;
        public DistrictController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> District()
        {
            var districts = await _context.District.ToListAsync();
            return View("~/Views/Admin/DefiningData/District.cshtml", districts);
        }
        //Add a District
        [HttpPost]
        public async Task<IActionResult> AddDistrict(DistrictModel district)
        {
            var existingSection = await _context.District.FindAsync(district.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The District ID already exists.");
                return View("~/Views/Admin/DefiningData/District.cshtml", await _context.District.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.District.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction("District");
            }
            return View("~/Views/Admin/DefiningData/District.cshtml", await _context.District.ToListAsync());
        }
        //Delete a District
        [HttpPost]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            var district = await _context.District.FindAsync(id);
            if (district != null)
            {
                _context.District.Remove(district);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("District");
        }

    }
}
