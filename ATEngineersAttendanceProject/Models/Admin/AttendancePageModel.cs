using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATEngineersAttendanceProject.Models.Admin
{
    public class AttendancePageModel
    {
        [Key] 
        public int WorkId { get; set; }
        public int EngineerId { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Village { get; set; }
        public string Mandal { get; set; }
        public string District { get; set; }
        public byte[]? ImageColumn { get; set; }

        public byte[]? Report { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public int PONo { get; set; }
        public string POCode { get; set; }
        public byte[]? POCopy { get; set; }

        [Column(TypeName = "date")] // Ensures only the date is stored in SQL Server

        [DataType(DataType.Date)]   // Specifies the property as a date (optional for display formatting)
        public DateTime? PODate { get; set; }
        public DateTime? Alloted_date { get; set; }
        public string? Remarks { get; set; }
    }
}
