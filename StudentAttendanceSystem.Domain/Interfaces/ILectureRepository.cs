using StudentAttendanceSystem.Domain.Entities;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Domain.Interfaces
{
    public interface ILectureRepository : IRepository<Lecture>
    {
        Task<Lecture> GetByQrCodeAsync(string qrCode);
    }
}
