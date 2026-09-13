using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Application.DTOs
{
    public class CreateLectureDto
    {
        [Required(ErrorMessage = "Course ID is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Lecture date is required")]
        public DateTime LectureDate { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeSpan EndTime { get; set; }
    }
}
