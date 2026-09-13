using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Interfaces;
using StudentAttendanceSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Infrastructure.Repositories
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        public LectureRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Lecture> GetByQrCodeAsync(string qrCode)
        {
            return await _dbSet.FirstOrDefaultAsync(l => l.QRCode == qrCode);
        }
    }
}
