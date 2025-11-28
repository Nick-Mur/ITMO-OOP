// CourseServiceTests.cs
using System;
using System.Linq;
using University.Core.Models;
using University.Core.Courses;
using University.Core.Services;
using Xunit;

namespace University.Tests
{
    /// <summary>
    /// Набор тестов для проверки логики сервиса управления курсами.
    /// </summary>
    public class CourseServiceTests
    {
        /// <summary>
        /// Проверяет, что онлайн-курс создаётся и сохраняется.
        /// </summary>
        [Fact]
        public void CreateOnlineCourse_AddsCourseToStorage()
        {
            ICourseService service = new CourseService();

            var course = service.CreateOnlineCourse("C# Базовый", "Moodle", "https://example.com");

            var loaded = service.GetCourse(course.Id);
            Assert.NotNull(loaded);
            var online = Assert.IsType<OnlineCourse>(loaded);
            Assert.Equal("C# Базовый", online.Title);
        }

        /// <summary>
        /// Проверяет, что курс удаляется из хранилища.
        /// </summary>
        [Fact]
        public void RemoveCourse_RemovesExistingCourse()
        {
            ICourseService service = new CourseService();
            var course = service.CreateOnlineCourse("C# Базовый", "Moodle", "https://example.com");

            var result = service.RemoveCourse(course.Id);
            var loaded = service.GetCourse(course.Id);

            Assert.True(result);
            Assert.Null(loaded);
        }

        /// <summary>
        /// Проверяет, что преподаватель корректно назначается через сервис.
        /// </summary>
        [Fact]
        public void AssignTeacherToCourse_AssignsCorrectTeacher()
        {
            ICourseService service = new CourseService();
            var course = service.CreateOnlineCourse("C# Базовый", "Moodle", "https://example.com");
            var teacher = new Teacher("Иван Иванов");

            service.AssignTeacherToCourse(course.Id, teacher);
            var loaded = service.GetCourse(course.Id);

            Assert.NotNull(loaded);
            Assert.NotNull(loaded.Teacher);
            Assert.Equal(teacher.Id, loaded.Teacher.Id);
        }

        /// <summary>
        /// Проверяет, что студент записывается на курс через сервис.
        /// </summary>
        [Fact]
        public void EnrollStudentToCourse_AddsStudent()
        {
            ICourseService service = new CourseService();
            var course = service.CreateOnlineCourse("C# Базовый", "Moodle", "https://example.com");
            var student = new Student("Студент 1");

            service.EnrollStudentToCourse(course.Id, student);

            var loaded = service.GetCourse(course.Id);
            Assert.Single(loaded.Students);
            Assert.Equal(student.Id, loaded.Students.First().Id);
        }

        /// <summary>
        /// Проверяет, что фильтрация курсов по преподавателю работает корректно.
        /// </summary>
        [Fact]
        public void GetCoursesByTeacher_ReturnsOnlyCoursesOfThisTeacher()
        {
            ICourseService service = new CourseService();
            var teacher1 = new Teacher("Преподаватель 1");
            var teacher2 = new Teacher("Преподаватель 2");

            var c1 = service.CreateOnlineCourse("Курс 1", "Moodle", "https://example.com/1");
            var c2 = service.CreateOfflineCourse("Курс 2", "Корпус А", "101");
            var c3 = service.CreateOnlineCourse("Курс 3", "Moodle", "https://example.com/3");

            service.AssignTeacherToCourse(c1.Id, teacher1);
            service.AssignTeacherToCourse(c2.Id, teacher1);
            service.AssignTeacherToCourse(c3.Id, teacher2);

            var teacher1Courses = service.GetCoursesByTeacher(teacher1.Id);

            Assert.Equal(2, teacher1Courses.Count);
            Assert.Contains(teacher1Courses, c => c.Id == c1.Id);
            Assert.Contains(teacher1Courses, c => c.Id == c2.Id);
            Assert.DoesNotContain(teacher1Courses, c => c.Id == c3.Id);
        }
    }
}
