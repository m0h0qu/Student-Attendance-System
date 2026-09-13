using Microsoft.AspNetCore.Mvc;
using StudentAttendanceSystem.Application.DTOs;
using StudentAttendanceSystem.Application.Interfaces;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LecturesController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        // إنشاء محاضرة جديدة
        [HttpPost]
        public async Task<IActionResult> CreateLecture([FromBody] CreateLectureDto lectureDto)
        {
            try
            {
                var result = await _lectureService.CreateLectureAsync(lectureDto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // الحصول على جميع المحاضرات
        [HttpGet]
        public async Task<IActionResult> GetAllLectures()
        {
            try
            {
                var result = await _lectureService.GetAllLecturesAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // الحصول على محاضرة محددة
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLectureById(int id)
        {
            try
            {
                var result = await _lectureService.GetLectureByIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // تحديث بيانات المحاضرة
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLecture(int id, [FromBody] CreateLectureDto lectureDto)
        {
            try
            {
                var result = await _lectureService.UpdateLectureAsync(id, lectureDto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // حذف المحاضرة
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            try
            {
                await _lectureService.DeleteLectureAsync(id);
                return Ok("Lecture deleted successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // تفعيل المحاضرة
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateLecture(int id)
        {
            try
            {
                var result = await _lectureService.ActivateLectureAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // إلغاء تفعيل المحاضرة
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateLecture(int id)
        {
            try
            {
                var result = await _lectureService.DeactivateLectureAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
