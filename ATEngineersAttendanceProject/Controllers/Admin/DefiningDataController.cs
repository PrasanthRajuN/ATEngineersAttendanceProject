using Microsoft.AspNetCore.Mvc;

namespace ATEngineersAttendanceProject.Controllers.Admin
{
    public class DefiningDataController : Controller
    {
        public IActionResult DefiningPage()
        {
            return View("~/Views/Admin/DefiningPage.cshtml");
        }
    }
}
