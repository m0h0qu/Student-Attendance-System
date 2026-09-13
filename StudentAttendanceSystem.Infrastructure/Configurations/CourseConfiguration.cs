using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAttendanceSystem.Domain.Entities;

namespace StudentAttendanceSystem.Infrastructure.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CourseCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(c => c.CourseCode)
                .IsUnique();

            builder.Property(c => c.TeacherId)
                .IsRequired();

            builder.HasOne(c => c.Teacher)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Lectures)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
