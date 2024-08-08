namespace miniProject_API.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public string Image { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
