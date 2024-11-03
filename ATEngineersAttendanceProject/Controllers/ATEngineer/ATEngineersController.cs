using Microsoft.AspNetCore.Mvc;

namespace ATEngineersAttendanceProject.Controllers.ATEngineer
{
    public class ATEngineersController : Controller
    {
        public IActionResult ATEngineersPage()
        {
            return View("~/Views/ATEngineers/ATEngineersPage.cshtml");
        }
    }
}
