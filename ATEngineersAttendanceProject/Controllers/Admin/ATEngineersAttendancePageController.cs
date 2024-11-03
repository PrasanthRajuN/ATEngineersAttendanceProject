using ATEngineersAttendanceProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ATEngineersAttendanceProject.Controllers.Admin
{                                                                                   
    public class ATEngineersAttendancePageController : Controller
    {
        private readonly AppDbContext _context;

        public ATEngineersAttendancePageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ATEngineersAttendancePage()
        {
            var Attendance = await _context.AttendancePage.Where(a => a.ImageColumn != null).ToListAsync();
            return View("~/Views/Admin/ATEngineersAttendancePage.cshtml", Attendance);
        }

        public IActionResult GetImage(int id)
        {
            var record = _context.AttendancePage.FirstOrDefault(a => a.WorkId == id);
            if (record != null && record.ImageColumn != null)
            {
                return File(record.ImageColumn, "image/jpeg"); 
            }
            return NotFound(); 
        }

        [HttpGet]
        public IActionResult GetReportFile(int id)
        {
            var report = _context.AttendancePage.FirstOrDefault(a => a.WorkId == id);

            if (report != null && report.Report != null)
            {
                string fileName = "Report_" + id + ".pdf"; 
                string contentType = "application/pdf"; 

                return File(report.Report, contentType, fileName); 
            }
            return NotFound("Report file not found."); 
        }
    }
}
