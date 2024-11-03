using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ATEngineersAttendanceProject.Controllers.ATEngineer
{
    public class UploadController : Controller
    {
        private readonly AppDbContext _context;

        public UploadController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> UploadPage(int Attendanceid)
        {
            ViewBag.message = Attendanceid;
            return View("~/Views/ATEngineers/UploadPage.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> UploadPage(int id, IFormFile report, string image, double latitude, double longitude, string remarks)
        {
            var record = await _context.AttendancePage.FirstOrDefaultAsync(a => a.WorkId == id);

            if (record == null)
            {
                ViewBag.ToastMessage = "Record not found.";
                ViewBag.ToastType = "danger";
                ViewBag.message = id;
                return View("~/Views/ATEngineers/UploadPage.cshtml");
            }

            if (report != null && record.Report == null)
            {
                byte[] fileData;
                using (var memoryStream = new MemoryStream())
                {
                    await report.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();
                }
                record.Report = fileData;
                ViewBag.ToastMessage = "Report uploaded successfully.";
                ViewBag.ToastType = "success";
            }
            else if (report == null)
            {
                ViewBag.ToastMessage = "No file uploaded.";
                ViewBag.ToastType = "warning";
            }

            record.Remarks = remarks;
            record.ImageColumn = Convert.FromBase64String(image);
            record.Latitude = latitude;
            record.Longitude = longitude;
            await _context.SaveChangesAsync();

            ViewBag.ToastMessage = "Image updated successfully.";
            ViewBag.ToastType = "success";
            ViewBag.message = id;

            return View("~/Views/ATEngineers/UploadPage.cshtml");
        }

    }
}
