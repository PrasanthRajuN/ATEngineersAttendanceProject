using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.Admin
{
    public class AttendancePageController : Controller
    {
        private readonly AppDbContext _context;

        public AttendancePageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AttendancePage()
        {
            var Attendance = await _context.AttendancePage.ToListAsync();
            return View("~/Views/Admin/AttendancePage.cshtml", Attendance);
        }
    }
}
