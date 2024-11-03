using Microsoft.EntityFrameworkCore;
using ATEngineersAttendanceProject.Models.AdminModels;
using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using ATEngineersAttendanceProject.Models.ATEngineers;
using ATEngineersAttendanceProject.Models.Admin; 
namespace ATEngineersAttendanceProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<DistrictModel> District { get; set; }
        public DbSet<DepartmentModel> Department {  get; set; }

        public DbSet<MandalModel> Mandal { get; set; }

        public DbSet<VillageModel> Village { get; set; }

        public DbSet<LocationModel> Location { get; set; }
        public DbSet<ATEngineerModel> ATEngineer { get; set; }

        public DbSet<AttendancePageModel> AttendancePage { get; set; }
        public DbSet<ATEngineersAttendancePageModel> ATEngineersAttendancePage { get; set; }
        public DbSet<UploadPageModel> Uploads { get; set; }
        public DbSet<LocationsModel> Locations { get; set; }
        public DbSet<WorkIdCounter> WorkIdCounter   { get; set; }
    }
}

