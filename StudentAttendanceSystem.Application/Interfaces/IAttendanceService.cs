using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAttendanceSystem.Application.DTOs;

namespace StudentAttendanceSystem.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> ScanQrCodeAsync(ScanQrCodeDto scanDto);
        Task<IEnumerable<AttendanceDto>> GetStudentAttendanceAsync(int studentId);
        Task<IEnumerable<AttendanceDto>> GetLectureAttendanceAsync(int lectureId);
    }
}
