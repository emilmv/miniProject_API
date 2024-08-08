using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using miniProject_API.Entities;

namespace miniProject_API.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder
                 .Property(d => d.Name)
                 .IsRequired()
                 .HasMaxLength(20);
            builder
                .Property(d => d.ExperienceYear)
                .IsRequired();
            builder
                .HasOne(d => d.Department)
                .WithMany(d => d.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
