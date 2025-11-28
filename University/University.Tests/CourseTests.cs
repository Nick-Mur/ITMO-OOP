// CourseTests.cs
using System;
using System.Linq;
using University.Core.Models;
using University.Core.Courses;
using University.Core.Services;
using Xunit;

namespace University.Tests
{
    /// <summary>
    /// Набор тестов для проверки логики курса.
    /// </summary>
    public class CourseTests
    {
        /// <summary>
        /// Проверяет, что студент успешно добавляется на курс.
        /// </summary>
        [Fact]
        public void EnrollStudent_AddsStudentToCourse()
        {
            var course = new OnlineCourse("C# Базовый", "Moodle", "https://example.com");
            var student = new Student("Студент 1");

            course.EnrollStudent(student);

            Assert.Single(course.Students);
            Assert.Equal(student.Id, course.Students.First().Id);
        }

        /// <summary>
        /// Проверяет, что повторная запись того же студента выбрасывает исключение.
        /// </summary>
        [Fact]
        public void EnrollStudent_WhenDuplicate_Throws()
        {
            var course = new OnlineCourse("C# Базовый", "Moodle", "https://example.com");
            var student = new Student("Студент 1");

            course.EnrollStudent(student);

            var ex = Assert.Throws<InvalidOperationException>(() => course.EnrollStudent(student));
            Assert.Equal("Студент уже записан на курс.", ex.Message);
        }

        /// <summary>
        /// Проверяет, что студент удаляется с курса.
        /// </summary>
        [Fact]
        public void RemoveStudent_RemovesExistingStudent()
        {
            var course = new OnlineCourse("C# Базовый", "Moodle", "https://example.com");
            var student = new Student("Студент 1");
            course.EnrollStudent(student);

            course.RemoveStudent(student.Id);

            Assert.Empty(course.Students);
        }

        /// <summary>
        /// Проверяет, что удаление несуществующего студента выбрасывает исключение.
        /// </summary>
        [Fact]
        public void RemoveStudent_WhenNotExists_Throws()
        {
            var course = new OnlineCourse("C# Базовый", "Moodle", "https://example.com");

            Assert.Throws<InvalidOperationException>(() => course.RemoveStudent(Guid.NewGuid()));
        }

        /// <summary>
        /// Проверяет, что преподаватель корректно назначается на курс.
        /// </summary>
        [Fact]
        public void AssignTeacher_SetsTeacher()
        {
            var course = new OnlineCourse("C# Базовый", "Moodle", "https://example.com");
            var teacher = new Teacher("Преподаватель");

            course.AssignTeacher(teacher);

            Assert.NotNull(course.Teacher);
            Assert.Equal(teacher.Id, course.Teacher.Id);
        }
    }
}
