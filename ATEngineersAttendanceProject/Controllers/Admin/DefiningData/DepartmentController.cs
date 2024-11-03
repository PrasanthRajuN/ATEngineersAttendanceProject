using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.Admin.DefiningData
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Department()
        {
            var departments = await _context.Department.ToListAsync();
            return View("~/Views/Admin/DefiningData/Department.cshtml", departments);
        }
        //Add a Department
        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentModel department)
        {
            var existingSection = await _context.Department.FindAsync(department.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/Admin/DefiningData/Department.cshtml", await _context.Department.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Department.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Department");
            }
            return View("~/Views/Admin/DefiningData/Department.cshtml", await _context.Department.ToListAsync());
        }
        //Delete a Department
        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Department");
        }

    }
}
