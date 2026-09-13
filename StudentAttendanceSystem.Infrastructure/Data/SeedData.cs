using StudentAttendanceSystem.Domain.Entities;
using StudentAttendanceSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAttendanceSystem.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Seed(AppDbContext context)
        {
            
            if (context.Users.Any())
            {
                return;
            }


            var users = new List<User>
            {
                new User
                {
                    Name = "Dr. Ahmed Mohamed",
                    Email = "ahmed.teacher@example.com",
                    Phone = "1234567890",
                    Password = HashPassword("Teacher123"),
                    Role = Role.Teacher
                },
                new User
                {
                    Name = "Dr. Sara Ali",
                    Email = "sara.teacher@example.com",
                    Phone = "0987654321",
                    Password = HashPassword("Teacher123"),
                    Role = Role.Teacher
                },
                new User
                {
                    Name = "Omar Hassan",
                    Email = "omar.student@example.com",
                    Phone = "1122334455",
                    Password = HashPassword("Student123"),
                    Role = Role.Student
                },
                new User
                {
                    Name = "Fatima Ahmed",
                    Email = "fatima.student@example.com",
                    Phone = "5566778899",
                    Password = HashPassword("Student123"),
                    Role = Role.Student
                },
                new User
                {
                    Name = "Khaled Omar",
                    Email = "khaled.student@example.com",
                    Phone = "9988776655",
                    Password = HashPassword("Student123"),
                    Role = Role.Student
                }
            };

            await context.Users.AddRangeAsync(users);
 await context.SaveChangesAsync();

            
            var courses = new List<Course>
            {
                new Course
                {
                    CourseName = "C# Programming",
                    CourseCode = "CS101",
                    TeacherId = users[0].Id 
                },
                new Course
                {
                    CourseName = "Web Development",
                    CourseCode = "CS102",
                    TeacherId = users[1].Id 
                },
                new Course
                {
                    CourseName = "Database Systems",
                    CourseCode = "CS103",
                    TeacherId = users[0].Id 
                }
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();

            // Seed Lectures
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    CourseId = courses[0].Id,
                    LectureDate = DateTime.Today.AddDays(1),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    QRCode = Guid.NewGuid().ToString(),
                    IsActive = true
                },
                new Lecture
                {
                    CourseId = courses[0].Id,
                    LectureDate = DateTime.Today.AddDays(3),
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(12, 30, 0),
                    QRCode = Guid.NewGuid().ToString(),
                    IsActive = true
                },
                new Lecture
                {
                    CourseId = courses[1].Id,
                    LectureDate = DateTime.Today.AddDays(2),
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(15, 30, 0),
                    QRCode = Guid.NewGuid().ToString(),
                    IsActive = true
                }
            };

            await context.Lectures.AddRangeAsync(lectures);
            await context.SaveChangesAsync();

            // Seed Attendance (some sample attendance records)
            var attendances = new List<Attendance>
            {
                new Attendance
                {
                    StudentId = users[2].Id, // Omar
                    LectureId = lectures[0].Id,
                    AttendanceTime = DateTime.Now.AddHours(-2),
                    Status = AttendanceStatus.Present
                },
                new Attendance
                {
                    StudentId = users[3].Id, // Fatima
                    LectureId = lectures[0].Id,
                    AttendanceTime = DateTime.Now.AddHours(-1),
                    Status = AttendanceStatus.Present
                }
            };

            await context.Attendances.AddRangeAsync(attendances);
            await context.SaveChangesAsync();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
