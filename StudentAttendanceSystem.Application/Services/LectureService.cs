using StudentAttendanceSystem.Application.DTOs;
using StudentAttendanceSystem.Application.Interfaces;
using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Application.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly ICourseRepository _courseRepository;

        public LectureService(ILectureRepository lectureRepository, ICourseRepository courseRepository)
        {
            _lectureRepository = lectureRepository;
            _courseRepository = courseRepository;
        }

        public async Task<LectureDto> CreateLectureAsync(CreateLectureDto lectureDto)
        {
            // Check if course exists
            var course = await _courseRepository.GetByIdAsync(lectureDto.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            // Generate unique QR Code
            string qrCode = Guid.NewGuid().ToString();

            var lecture = new Lecture
            {
                CourseId = lectureDto.CourseId,
                LectureDate = lectureDto.LectureDate,
                StartTime = lectureDto.StartTime,
                EndTime = lectureDto.EndTime,
                QRCode = qrCode,
                IsActive = true
            };

            await _lectureRepository.AddAsync(lecture);

            return new LectureDto
            {
                Id = lecture.Id,
                CourseId = lecture.CourseId,
                CourseName = course.CourseName,
                LectureDate = lecture.LectureDate,
                StartTime = lecture.StartTime,
                EndTime = lecture.EndTime,
                QRCode = lecture.QRCode,
                IsActive = lecture.IsActive
            };
        }

        public async Task<LectureDto> GetLectureByIdAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }

            var course = await _courseRepository.GetByIdAsync(lecture.CourseId);

            return new LectureDto
            {
                Id = lecture.Id,
                CourseId = lecture.CourseId,
                CourseName = course?.CourseName,
                LectureDate = lecture.LectureDate,
                StartTime = lecture.StartTime,
                EndTime = lecture.EndTime,
                QRCode = lecture.QRCode,
                IsActive = lecture.IsActive
            };
        }

        public async Task<IEnumerable<LectureDto>> GetAllLecturesAsync()
        {
            var lectures = await _lectureRepository.GetAllAsync();
            var result = new List<LectureDto>();

            foreach (var lecture in lectures)
            {
                var course = await _courseRepository.GetByIdAsync(lecture.CourseId);
                result.Add(new LectureDto
                {
                    Id = lecture.Id,
                    CourseId = lecture.CourseId,
                    CourseName = course?.CourseName,
                    LectureDate = lecture.LectureDate,
                    StartTime = lecture.StartTime,
                    EndTime = lecture.EndTime,
                    QRCode = lecture.QRCode,
                    IsActive = lecture.IsActive
                });
            }

            return result;
        }

        public async Task<LectureDto> UpdateLectureAsync(int id, CreateLectureDto lectureDto)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }

            var course = await _courseRepository.GetByIdAsync(lectureDto.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            lecture.CourseId = lectureDto.CourseId;
            lecture.LectureDate = lectureDto.LectureDate;
            lecture.StartTime = lectureDto.StartTime;
            lecture.EndTime = lectureDto.EndTime;
            await _lectureRepository.UpdateAsync(lecture);

            return new LectureDto
            {
                Id = lecture.Id,
                CourseId = lecture.CourseId,
                CourseName = course.CourseName,
                LectureDate = lecture.LectureDate,
                StartTime = lecture.StartTime,
                EndTime = lecture.EndTime,
                QRCode = lecture.QRCode,
                IsActive = lecture.IsActive
            };
        }

        public async Task<bool> DeleteLectureAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }

            await _lectureRepository.DeleteAsync(lecture);
            return true;
        }

        public async Task<LectureDto> ActivateLectureAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }

            lecture.IsActive = true;
            await _lectureRepository.UpdateAsync(lecture);

            var course = await _courseRepository.GetByIdAsync(lecture.CourseId);

            return new LectureDto
            {
                Id = lecture.Id,
                CourseId = lecture.CourseId,
                CourseName = course?.CourseName,
                LectureDate = lecture.LectureDate,
                StartTime = lecture.StartTime,
                EndTime = lecture.EndTime,
                QRCode = lecture.QRCode,
                IsActive = lecture.IsActive
            };
        }

        public async Task<LectureDto> DeactivateLectureAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }

            lecture.IsActive = false;
            await _lectureRepository.UpdateAsync(lecture);

            var course = await _courseRepository.GetByIdAsync(lecture.CourseId);

            return new LectureDto
            {
                Id = lecture.Id,
                CourseId = lecture.CourseId,
                CourseName = course?.CourseName,
                LectureDate = lecture.LectureDate,
                StartTime = lecture.StartTime,
                EndTime = lecture.EndTime,
                QRCode = lecture.QRCode,
                IsActive = lecture.IsActive
            };
        }
    }
}
