namespace miniProject_API.DTOs.DepartmentDTOs
{
    public class DepartmentReturnDTO
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public string Image { get; set; }
        public List<DoctorInDepartmentReturnDTO> Doctors { get; set; }
    }
    public class DoctorInDepartmentReturnDTO
    {
        public string Name { get; set; }
        public int ExperienceYear { get; set; }
    }
}
