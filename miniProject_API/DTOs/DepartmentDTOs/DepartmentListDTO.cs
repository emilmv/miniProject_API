using miniProject_API.Entities;

namespace miniProject_API.DTOs.DepartmentDTOs
{
    public class DepartmentListDTO
    {
        public DepartmentListDTO()
        {
            Departments = new();
        }

        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public List<DepartmentReturnDTO>Departments{ get; set; }
    }
}
