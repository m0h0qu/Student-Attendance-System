using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Interfaces;
using StudentAttendanceSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
