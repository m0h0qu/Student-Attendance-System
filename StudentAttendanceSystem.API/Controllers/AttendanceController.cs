using Microsoft.AspNetCore.Mvc;
using StudentAttendanceSystem.Application.DTOs;
using StudentAttendanceSystem.Application.Interfaces;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // مسح QR Code وتسجيل الحضور
        [HttpPost("scan")]
        public async Task<IActionResult> ScanQrCode([FromBody] ScanQrCodeDto scanDto)
        {
            try
            {
                var result = await _attendanceService.ScanQrCodeAsync(scanDto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // الحصول على سجل حضور طالب
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(int studentId)
        {
            try
            {
                var result = await _attendanceService.GetStudentAttendanceAsync(studentId);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // الحصول على حضور محاضرة
        [HttpGet("lecture/{lectureId}")]
        public async Task<IActionResult> GetLectureAttendance(int lectureId)
        {
            try
            {
                var result = await _attendanceService.GetLectureAttendanceAsync(lectureId);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
