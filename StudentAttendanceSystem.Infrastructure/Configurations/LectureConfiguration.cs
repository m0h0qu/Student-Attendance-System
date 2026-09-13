using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAttendanceSystem.Domain.Entities;

namespace StudentAttendanceSystem.Infrastructure.Configurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.CourseId)
                .IsRequired();

            builder.Property(l => l.LectureDate)
                .IsRequired();

            builder.Property(l => l.StartTime)
                .IsRequired();

            builder.Property(l => l.EndTime)
                .IsRequired();

            builder.Property(l => l.QRCode)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(l => l.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(l => l.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Attendances)
                .WithOne(a => a.Lecture)
                .HasForeignKey(a => a.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
