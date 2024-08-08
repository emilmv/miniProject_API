namespace miniProject_API.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExperienceYear { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
