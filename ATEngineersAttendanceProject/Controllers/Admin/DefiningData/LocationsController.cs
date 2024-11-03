using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class LocationsController : Controller
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Locations()
        {
            var locations = await _context.Locations.ToListAsync();
            return View("~/Views/Admin/DefiningData/Locations.cshtml", locations);
        }
        //Add a Location
        [HttpPost]
        public async Task<IActionResult> AddLocations(LocationsModel locations)
        {
            var existingSection = await _context.Locations.FindAsync(locations.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/Admin/DefiningData/Locations.cshtml", await _context.Locations.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Locations.Add(locations);
                await _context.SaveChangesAsync();
                return RedirectToAction("Locations");
            }
            return View("~/Views/Admin/DefiningData/Locations.cshtml", await _context.Locations.ToListAsync());
        }
        //Delete a Location
        [HttpPost]
        public async Task<IActionResult> DeleteLocations(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Locations");
        }
    }
}
