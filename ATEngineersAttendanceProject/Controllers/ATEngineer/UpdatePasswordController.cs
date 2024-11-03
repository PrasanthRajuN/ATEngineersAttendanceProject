using ATEngineersAttendanceProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.ATEngineer
{
    public class UpdatePasswordController : Controller
    {
        private readonly AppDbContext _context;
        public UpdatePasswordController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePassword()
        {

            string userIdString = HttpContext.Session.GetString("UserID");
            string password = HttpContext.Session.GetString("Password");
            int userId = int.Parse(userIdString);
            var engineer = await _context.ATEngineer.SingleOrDefaultAsync(a => a.Id == userId);
            return View("~/Views/ATEngineers/UpdatePassword.cshtml", engineer);
        }
        // Updating password based on ID
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string newPassword)
        {
            string userIdString = HttpContext.Session.GetString("UserID");
            int userId = int.Parse(userIdString);
            var engineer = await _context.ATEngineer.FindAsync(userId);

            if (engineer != null)
            {
                engineer.Password = newPassword;
                await _context.SaveChangesAsync();
                ViewBag.Message = "Successfully Updated";
                return View("~/Views/ATEngineers/UpdatePassword.cshtml", engineer);
            }

            ModelState.AddModelError("Id", "User ID not found.");
            return View("~/Views/ATEngineers/UpdatePassword.cshtml");
        }
    }
}
