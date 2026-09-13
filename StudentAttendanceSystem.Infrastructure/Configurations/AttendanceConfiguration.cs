using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Enums;

namespace StudentAttendanceSystem.Infrastructure.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.StudentId)
                .IsRequired();

            builder.Property(a => a.LectureId)
                .IsRequired();

            builder.Property(a => a.AttendanceTime)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasDefaultValue(AttendanceStatus.Present);

            // Unique constraint to prevent duplicate attendance
            builder.HasIndex(a => new { a.StudentId, a.LectureId })
                .IsUnique();

            builder.HasOne(a => a.Student)
                .WithMany(u => u.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Lecture)
                .WithMany(l => l.Attendances)
                .HasForeignKey(a => a.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
