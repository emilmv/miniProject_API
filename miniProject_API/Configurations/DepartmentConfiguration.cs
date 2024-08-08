using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using miniProject_API.Entities;

namespace miniProject_API.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder
                .Property(d => d.Limit)
                .IsRequired();
            builder
                .Property(d => d.Image)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
