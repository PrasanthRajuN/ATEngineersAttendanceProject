using ATEngineersAttendanceProject.Data;
using Microsoft.AspNetCore.Mvc;
namespace ATEngineersAttendanceProject.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
