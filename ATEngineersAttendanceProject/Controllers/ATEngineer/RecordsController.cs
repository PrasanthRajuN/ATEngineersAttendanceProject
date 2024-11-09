using ATEngineersAttendanceProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.ATEngineer
{
    public class RecordsController : Controller
    {
        private readonly AppDbContext _context;
        public RecordsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Records()
        {
            string userIdString = HttpContext.Session.GetString("UserID");
            string password = HttpContext.Session.GetString("Password");

            int userId = int.Parse(userIdString);
            ViewBag.UserId = userId;
            ViewBag.Password = password;
            var records = await _context.AttendancePage.Where(a => a.EngineerId == userId && a.ImageColumn!=null).ToListAsync();
            return View("~/Views/ATEngineers/Records.cshtml", records);
        }
    }
}
