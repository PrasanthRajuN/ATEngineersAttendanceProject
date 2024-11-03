using System.ComponentModel.DataAnnotations;
namespace ATEngineersAttendanceProject.Models.ATEngineers
{
    public class UploadPageModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] Image { get; set; }


        // Add these properties to store attendance data
        [Required]
        public string Name { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Village { get; set; }

        [Required]
        public string Mandal { get; set; }

        [Required]
        public string District { get; set; }
    }
}
