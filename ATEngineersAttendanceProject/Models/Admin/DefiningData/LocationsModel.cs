namespace ATEngineersAttendanceProject.Models.Admin.DefiningData
{
    public class LocationsModel
    {
        public int? Id { get; set; }
        public string District { get; set; }
        public string Mandal { get; set; }
        public string Village { get; set; }
        public string? Location { get; set; }
        public int DistrictId { get; set; }
        public int MandalId { get; set; }
    }
}
