using ATEngineersAttendanceProject.Models.Admin.DefiningData;
using ATEngineersAttendanceProject.Models.ATEngineers;

namespace ATEngineersAttendanceProject.Models.Admin
{
    public class CombinedViewModel
    {
        public IEnumerable<SectionModel> Sections { get; set; }
        public IEnumerable<DepartmentModel> Departments { get; set; }
        public IEnumerable<DistrictModel> District { get; set; }
        public IEnumerable<MandalModel> Mandal { get; set; }
        public IEnumerable<VillageModel> Village { get; set; }
        public IEnumerable<LocationModel> Location { get; set; }
        public IEnumerable<ATEngineerModel> ATEngineer { get; set; }
        public IEnumerable<LocationsModel> Locations { get; set; }
    }
}
