using System;
using System.Collections.Generic;

namespace StudentAttendanceSystem.Domain.Entities
{
    public class Lecture
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public DateTime LectureDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string QRCode { get; set; }
        public bool IsActive { get; set; }

        
        public virtual Course Course { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

        public Lecture()
        {
            Attendances = new HashSet<Attendance>();
            IsActive = true;
        }
    }
}
