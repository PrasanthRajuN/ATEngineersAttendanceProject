using ATEngineersAttendanceProject.Data;
using ATEngineersAttendanceProject.Models.Admin;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATEngineersAttendanceProject.Controllers.Admin
{
    public class AllotworkController : Controller
    {
        private readonly AppDbContext _context;

        public AllotworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Fetch and display sections
        public async Task<IActionResult> AllotWork()
        {
            var viewModel = new CombinedViewModel
            {
                Sections = await _context.Sections.ToListAsync(),
                Departments = await _context.Department.ToListAsync(),
                District = await _context.District.ToListAsync(),
                Mandal = await _context.Mandal.ToListAsync(),
                Village = await _context.Village.ToListAsync(),
                Location = await _context.Location.ToListAsync(),
                ATEngineer = await _context.ATEngineer.ToListAsync(),
                Locations = await _context.Locations.GroupBy(l => l.District).Select(g => g.First()).Distinct().ToListAsync()
            };
            return View("~/Views/Admin/AllotWork.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AllotWork(DateTime alloted_date, string pocode, int pono, DateTime podate, IFormFile pocopy, int atEngineerId, string section, string department, string district, string mandal, string village, string location)
        {
            if (string.IsNullOrWhiteSpace(pocode) || podate == default || pocopy == null ||
                string.IsNullOrWhiteSpace(section) || string.IsNullOrWhiteSpace(department) || string.IsNullOrWhiteSpace(district) ||
                string.IsNullOrWhiteSpace(mandal) || string.IsNullOrWhiteSpace(village) || string.IsNullOrWhiteSpace(location))
            {
                ViewBag.AlertMessage = "Please select all required fields.";
                return await AllotWork();
            }
            var atEngineer = await _context.ATEngineer.FindAsync(atEngineerId);


            // Retrieve location details
            string atengineerid = atEngineerId.ToString("D2");
            var districtLocations = await _context.Locations
                .FirstOrDefaultAsync(l => l.District == district); // Assuming 'District' is a property in LocationsModel
            var mandalLocations = await _context.Locations
                .FirstOrDefaultAsync(l => l.Mandal == mandal);
            string districtids = districtLocations.DistrictId.ToString("D2");
            string mandalids = mandalLocations.MandalId.ToString("D2");
            var counter = _context.WorkIdCounter.FirstOrDefault();
            int locationCount = counter.CurrentCount;
            string formattedLocationCount = locationCount.ToString("D2");

            string workId = $"{atengineerid}{districtids}{mandalids}{formattedLocationCount}";
            int workIdInt = int.Parse(workId); 

            // Update and save the counter
            counter.CurrentCount = locationCount;

            counter.CurrentCount = counter.CurrentCount+1 ;       
            if (atEngineer == null)
            {
                ViewBag.AlertMessage = "AT Engineer not found.";
                return await AllotWork();
            }

            byte[] fileData = null;
            if (pocopy != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await pocopy.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();  
                }
            }
            else
            {
                ViewBag.AlertMessage = "Postal Copy file is required.";
                return await AllotWork();
            }

            var workEntry = new AttendancePageModel
            {
                Alloted_date = alloted_date,
                PONo = pono,
                POCode = pocode,
                PODate = podate,
                POCopy = fileData,
                EngineerId = atEngineerId,
                Section = section,
                Department = department,
                District = district,
                Mandal = mandal,
                Village = village,
                Name = atEngineer.Name, 
                Location = location,
                WorkId= workIdInt
            };
            _context.AttendancePage.Add(workEntry);
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Work alloted successful!";
            return await AllotWork();
        }
        [HttpGet]
        public async Task<IActionResult> GetMandals(string district)
        {
            if (string.IsNullOrEmpty(district))
            {
                return Json(new { message = "No district selected." });
            }
            var mandals = await _context.Locations
                .Where(l => l.District == district)
                .Select(l => l.Mandal)
                .Distinct()
                .ToListAsync();

            return Json(mandals);
        }
        [HttpGet]
        public async Task<IActionResult> GetVillages(string mandal)
        {
            if (string.IsNullOrEmpty(mandal))
            {
                return Json(new { message = "No district selected." });
            }
            var villages = await _context.Locations
                .Where(l => l.Mandal == mandal)
                .Select(l => l.Village)
                .Distinct()
                .ToListAsync();

            return Json(villages);
        }
        [HttpGet]
        public async Task<IActionResult> GetLocations(string village)
        {
            if (string.IsNullOrEmpty(village))
            {
                return Json(new { message = "No district selected." });
            }
            var locations = await _context.Locations
                .Where(l => l.Village == village)
                .Select(l => l.Location)
                .Distinct()
                .ToListAsync();

            return Json(locations);
        }

    }
}
