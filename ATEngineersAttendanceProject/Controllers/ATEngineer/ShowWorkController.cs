using ATEngineersAttendanceProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.ATEngineer
{
    public class ShowWorkController : Controller
    {
        private readonly AppDbContext _context;
        public ShowWorkController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ShowWork()
        {
            string userIdString = HttpContext.Session.GetString("UserID");
            string password = HttpContext.Session.GetString("Password");

            int userId = int.Parse(userIdString);
            ViewBag.UserId = userId;
            ViewBag.Password = password;
            var works = await _context.AttendancePage.Where(a => a.EngineerId == userId).ToListAsync();
            return View("~/Views/ATEngineers/ShowWork.cshtml", works);
        }
    }
}
