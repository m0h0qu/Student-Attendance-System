using System.Collections.Generic;
using StudentAttendanceSystem.Domain.Enums;

namespace StudentAttendanceSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        // Navigation Properties
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

        public User()
        {
            Courses = new HashSet<Course>();
            Attendances = new HashSet<Attendance>();
        }
    }
}
