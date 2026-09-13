using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAttendanceSystem.Application.DTOs;

namespace StudentAttendanceSystem.Application.Interfaces
{
    public interface ILectureService
    {
        Task<LectureDto> CreateLectureAsync(CreateLectureDto lectureDto);
        Task<LectureDto> GetLectureByIdAsync(int id);
        Task<IEnumerable<LectureDto>> GetAllLecturesAsync();
        Task<LectureDto> UpdateLectureAsync(int id, CreateLectureDto lectureDto);
        Task<bool> DeleteLectureAsync(int id);
        Task<LectureDto> ActivateLectureAsync(int id);
        Task<LectureDto> DeactivateLectureAsync(int id);
    }
}
