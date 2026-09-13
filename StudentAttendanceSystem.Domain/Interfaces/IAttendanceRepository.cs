using StudentAttendanceSystem.Domain.Entities;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Domain.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<Attendance> GetByStudentAndLectureAsync(int studentId, int lectureId);
    }
}
