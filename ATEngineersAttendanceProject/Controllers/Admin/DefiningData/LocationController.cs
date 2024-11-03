using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class LocationController : Controller
    {

        private readonly AppDbContext _context;
        public LocationController(AppDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Location()
        {
            var locations = await _context.Location.ToListAsync();
            return View("~/Views/Admin/DefiningData/Location.cshtml", locations);
        }
        //Add a Location
        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationModel location)
        {
            var existingSection = await _context.Location.FindAsync(location.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The location ID already exists.");
                return View("~/Views/Admin/DefiningData/Location.cshtml", await _context.Location.ToListAsync());
            }
            if (ModelState.IsValid)
            {
                _context.Location.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction("Location");
            }
            return View("~/Views/Admin/DefiningData/Location.cshtml", await _context.Location.ToListAsync());
        }
        //Delete a Location
        [HttpPost]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location != null)
            {
                _context.Location.Remove(location);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Location");

        }

    }
}
