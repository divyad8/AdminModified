using Microsoft.EntityFrameworkCore;
using AdminPageProject.Models; // Add this to the top of your DbContext file
namespace AdminPageProject.Data
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

        public DbSet<AttendencePageModel> AttendencePage { get; set; }
        public DbSet<ATEngineersAttendencePageModel> ATEngineersAttendencePage { get; set; }
        public DbSet<UploadPageModel> Uploads { get; set; }

    }
}
