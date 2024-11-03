namespace ATEngineersAttendanceProject.Models.Admin
{
    public class ATEngineersAttendancePageModel
    {
        public int Id { get; set; }
        public string ATEngineerName { get; set; }
        public string Location { get; set; }
        public string Village { get; set; }
        public string Mandal { get; set; }
        public string District { get; set; }
        public byte[] UploadATReport { get; set; }
        public byte[] UploadSelfie { get; set; }

    }
}
