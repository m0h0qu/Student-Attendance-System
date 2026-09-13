using System;
using StudentAttendanceSystem.Domain.Enums;

namespace StudentAttendanceSystem.Application.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int LectureId { get; set; }
        public DateTime AttendanceTime { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
