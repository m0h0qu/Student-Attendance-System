using StudentAttendanceSystem.Application.DTOs;
using StudentAttendanceSystem.Application.Interfaces;
using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Enums;
using StudentAttendanceSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILectureRepository _lectureRepository;
        private readonly IUserRepository _userRepository;

        public AttendanceService(
            IAttendanceRepository attendanceRepository,
            ILectureRepository lectureRepository,
            IUserRepository userRepository)
        {
            _attendanceRepository = attendanceRepository;
            _lectureRepository = lectureRepository;
            _userRepository = userRepository;
        }

        public async Task<AttendanceDto> ScanQrCodeAsync(ScanQrCodeDto scanDto)
        {
            // Check if student exists
            var student = await _userRepository.GetByIdAsync(scanDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            // Check if student role is Student
            if (student.Role != Role.Student)
            {
                throw new Exception("User is not a student");
            }

            // Find lecture by QR Code
            var lecture = await _lectureRepository.GetByQrCodeAsync(scanDto.QRCode);
            if (lecture == null)
            {
                throw new Exception("Invalid QR Code");
            }

            // Check if lecture is active
            if (!lecture.IsActive)
            {
                throw new Exception("Lecture is not active");
            }

            // Check if attendance already exists
            var existingAttendance = await _attendanceRepository.GetByStudentAndLectureAsync(scanDto.StudentId, lecture.Id);
            if (existingAttendance != null)
            {
                throw new Exception("Attendance already recorded");
            }

            // Create attendance record
            var attendance = new Attendance
            {
                StudentId = scanDto.StudentId,
                LectureId = lecture.Id,
                AttendanceTime = DateTime.Now,
                Status = AttendanceStatus.Present
            };

            await _attendanceRepository.AddAsync(attendance);

            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                StudentName = student.Name,
                LectureId = attendance.LectureId,
                AttendanceTime = attendance.AttendanceTime,
                Status = attendance.Status
            };
        }

        public async Task<IEnumerable<AttendanceDto>> GetStudentAttendanceAsync(int studentId)
        {
            // Check if student exists
            var student = await _userRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            var attendances = await _attendanceRepository.FindAsync(a => a.StudentId == studentId);
            var result = new List<AttendanceDto>();

            foreach (var attendance in attendances)
            {
                var lecture = await _lectureRepository.GetByIdAsync(attendance.LectureId);
                result.Add(new AttendanceDto
                {
                    Id = attendance.Id,
                    StudentId = attendance.StudentId,
                    StudentName = student.Name,
                    LectureId = attendance.LectureId,
                    AttendanceTime = attendance.AttendanceTime,
                    Status = attendance.Status
                });
            }

            return result;
        }

        public async Task<IEnumerable<AttendanceDto>> GetLectureAttendanceAsync(int lectureId)
        {
            // Check if lecture exists
            var lecture = await _lectureRepository.GetByIdAsync(lectureId);
            if (lecture == null)
            {
                throw new Exception("Lecture not found");
            }

            var attendances = await _attendanceRepository.FindAsync(a => a.LectureId == lectureId);
            var result = new List<AttendanceDto>();

            foreach (var attendance in attendances)
            {
                var student = await _userRepository.GetByIdAsync(attendance.StudentId);
                result.Add(new AttendanceDto
                {
                    Id = attendance.Id,
                    StudentId = attendance.StudentId,
                    StudentName = student?.Name,
                    LectureId = attendance.LectureId,
                    AttendanceTime = attendance.AttendanceTime,
                    Status = attendance.Status
                });
            }

            return result;
        }
    }
}
