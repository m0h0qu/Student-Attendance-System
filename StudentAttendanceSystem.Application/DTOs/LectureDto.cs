using System;

namespace StudentAttendanceSystem.Application.DTOs
{
    public class LectureDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime LectureDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string QRCode { get; set; }
        public bool IsActive { get; set; }
    }
}
