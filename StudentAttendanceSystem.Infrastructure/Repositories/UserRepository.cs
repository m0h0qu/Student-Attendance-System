using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Interfaces;
using StudentAttendanceSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
