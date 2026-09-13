using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAttendanceSystem.Application.DTOs;

namespace StudentAttendanceSystem.Application.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto);
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> UpdateCourseAsync(int id, CreateCourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
