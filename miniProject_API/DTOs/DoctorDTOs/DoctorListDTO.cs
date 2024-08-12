using miniProject_API.DTOs.DepartmentDTOs;

namespace miniProject_API.DTOs.DoctorDTOs
{
    public class DoctorListDTO
    {
        public DoctorListDTO()
        {
            Doctors = new();
        }

        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public List<DoctorReturnDTO> Doctors { get; set; }
    }
}
