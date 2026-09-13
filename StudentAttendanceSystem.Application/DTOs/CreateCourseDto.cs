using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Application.DTOs
{
    public class CreateCourseDto
    {
        [Required(ErrorMessage = "Course name is required")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Teacher ID is required")]
        public int TeacherId { get; set; }
    }
}
