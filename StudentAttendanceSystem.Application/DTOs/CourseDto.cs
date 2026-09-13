using System;

namespace StudentAttendanceSystem.Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
