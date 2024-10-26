using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPageProject.Models
{
    public class AttendencePageModel
    {
        public int Id { get; set; }
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
        public int postal_no { get; set; }
        public string postal_code { get; set; }
        public byte[]? postal_copy { get; set; }
        [Column(TypeName = "date")] // Ensures only the date is stored in SQL Server
        [DataType(DataType.Date)]   // Specifies the property as a date (optional for display formatting)
        public DateTime? postal_date { get; set; }
    }
}
