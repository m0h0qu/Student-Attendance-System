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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto)
        {
            // Check if teacher exists
            var teacher = await _userRepository.GetByIdAsync(courseDto.TeacherId);
            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }

            // Check if course code already exists
            var existingCourses = await _courseRepository.FindAsync(c => c.CourseCode == courseDto.CourseCode);
            if (existingCourses.Any())
            {
                throw new Exception("Course code already exists");
            }

            var course = new Course
            {
                CourseName = courseDto.CourseName,
                CourseCode = courseDto.CourseCode,
                TeacherId = courseDto.TeacherId
            };

            await _courseRepository.AddAsync(course);

            return new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                TeacherId = course.TeacherId,
                TeacherName = teacher.Name
            };
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            var teacher = await _userRepository.GetByIdAsync(course.TeacherId);

            return new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                TeacherId = course.TeacherId,
                TeacherName = teacher?.Name
            };
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var result = new List<CourseDto>();

            foreach (var course in courses)
            {
                var teacher = await _userRepository.GetByIdAsync(course.TeacherId);
                result.Add(new CourseDto
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    CourseCode = course.CourseCode,
                    TeacherId = course.TeacherId,
                    TeacherName = teacher?.Name
                });
            }

            return result;
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, CreateCourseDto courseDto)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            // Check if teacher exists
            var teacher = await _userRepository.GetByIdAsync(courseDto.TeacherId);
            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }

            // Check if course code already exists for another course
            var existingCourses = await _courseRepository.FindAsync(c => c.CourseCode == courseDto.CourseCode && c.Id != id);
            if (existingCourses.Any())
            {
                throw new Exception("Course code already exists");
            }

            course.CourseName = courseDto.CourseName;
            course.CourseCode = courseDto.CourseCode;
            course.TeacherId = courseDto.TeacherId;

            await _courseRepository.UpdateAsync(course);

            return new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                TeacherId = course.TeacherId,
                TeacherName = teacher.Name
            };
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            await _courseRepository.DeleteAsync(course);
            return true;
        }
    }
}
