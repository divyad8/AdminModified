namespace AdminPageProject.Models
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

    }
}
