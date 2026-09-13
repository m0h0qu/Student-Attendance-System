using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAttendanceSystem.Application.DTOs;

namespace StudentAttendanceSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(CreateUserDto userDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> UpdateUserAsync(int id, CreateUserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
