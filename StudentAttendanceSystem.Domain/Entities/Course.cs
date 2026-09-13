using System.Collections.Generic;

namespace StudentAttendanceSystem.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int TeacherId { get; set; }

        // Navigation Properties
        public virtual User Teacher { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }

        public Course()
        {
            Lectures = new HashSet<Lecture>();
        }
    }
}
