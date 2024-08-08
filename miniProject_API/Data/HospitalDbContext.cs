using Microsoft.EntityFrameworkCore;
using miniProject_API.Entities;
using System.Reflection;

namespace miniProject_API.Data
{
    public class HospitalDbContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }




        public HospitalDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
