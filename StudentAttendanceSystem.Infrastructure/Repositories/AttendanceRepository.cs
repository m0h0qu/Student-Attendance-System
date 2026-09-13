using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Interfaces;
using StudentAttendanceSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Infrastructure.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Attendance> GetByStudentAndLectureAsync(int studentId, int lectureId)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.StudentId == studentId && a.LectureId == lectureId);
        }
    }
}
