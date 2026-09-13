using System;
using StudentAttendanceSystem.Domain.Enums;

namespace StudentAttendanceSystem.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LectureId { get; set; }
        public DateTime AttendanceTime { get; set; }
        public AttendanceStatus Status { get; set; }

        // Navigation Properties
        public virtual User Student { get; set; }
        public virtual Lecture Lecture { get; set; }
    }
}
