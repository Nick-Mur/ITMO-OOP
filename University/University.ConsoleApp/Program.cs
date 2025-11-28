using System;
using System.Collections.Generic;
using System.Linq;
using University.Core.Courses;
using University.Core.Models;
using University.Core.Services;

namespace University.ConsoleApp
{
    /// <summary>
    /// Точка входа консольного приложения.
    /// </summary>
    internal class Program
    {
        private static readonly ICourseService _service = new CourseService();

        // Простые in-memory справочники, чтобы назначать/записывать по Id
        private static readonly Dictionary<Guid, Teacher> _teachers = new();
        private static readonly Dictionary<Guid, Student> _students = new();

        /// <summary>
        /// Основной метод запуска приложения.
        /// </summary>
        private static void Main()
        {
            SeedDemoData();

            bool run = true;
            while (run)
            {
                Console.WriteLine();
                Console.WriteLine("=== Университет: курсы и преподаватели ===");
                Console.WriteLine("1. Создать онлайн-курс");
                Console.WriteLine("2. Создать офлайн-курс");
                Console.WriteLine("3. Удалить курс");
                Console.WriteLine("4. Назначить преподавателя на курс");
                Console.WriteLine("5. Записать студента на курс");
                Console.WriteLine("6. Показать курсы преподавателя");
                Console.WriteLine("7. Показать детали курса");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                var key = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (key)
                    {
                        case "1":
                            CreateOnlineCourse();
                            break;
                        case "2":
                            CreateOfflineCourse();
                            break;
                        case "3":
                            RemoveCourse();
                            break;
                        case "4":
                            AssignTeacher();
                            break;
                        case "5":
                            EnrollStudent();
                            break;
                        case "6":
                            ShowTeacherCourses();
                            break;
                        case "7":
                            ShowCourseDetails();
                            break;
                        case "0":
                            run = false;
                            break;
                        default:
                            Console.WriteLine("Неизвестная команда.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        // ----------------------------- Операции (как в тестах) -----------------------------

        /// <summary>Создаёт онлайн-курс и сохраняет его в сервисе.</summary>
        private static void CreateOnlineCourse()
        {
            Console.Write("Название: ");
            var title = ReadNonEmpty();

            Console.Write("Платформа: ");
            var platform = ReadNonEmpty();

            Console.Write("URL: ");
            var url = ReadNonEmpty();

            var course = _service.CreateOnlineCourse(title, platform, url);
            Console.WriteLine($"Создан онлайн-курс: {course.Title} | Id={course.Id}");
        }

        /// <summary>Создаёт офлайн-курс и сохраняет его в сервисе.</summary>
        private static void CreateOfflineCourse()
        {
            Console.Write("Название: ");
            var title = ReadNonEmpty();

            Console.Write("Корпус: ");
            var building = ReadNonEmpty();

            Console.Write("Аудитория: ");
            var room = ReadNonEmpty();

            var course = _service.CreateOfflineCourse(title, building, room);
            Console.WriteLine($"Создан офлайн-курс: {course.Title} | Id={course.Id}");
        }

        /// <summary>Удаляет курс по Id.</summary>
        private static void RemoveCourse()
        {
            var id = ReadGuid("Id курса: ");
            var ok = _service.RemoveCourse(id);
            Console.WriteLine(ok ? "Курс удалён." : "Курс не найден.");
        }

        /// <summary>Назначает преподавателя на курс.</summary>
        private static void AssignTeacher()
        {
            var courseId = ReadGuid("Id курса: ");

            Console.WriteLine("Выберите преподавателя или создайте нового:");
            ListTeachers();

            Console.Write("Введите Id существующего преподавателя или пусто для создания: ");
            var input = Console.ReadLine();

            Teacher teacher;
            if (!string.IsNullOrWhiteSpace(input) && Guid.TryParse(input, out var teacherId) && _teachers.TryGetValue(teacherId, out var t))
            {
                teacher = t;
            }
            else
            {
                Console.Write("Имя преподавателя: ");
                teacher = new Teacher(ReadNonEmpty());
                _teachers[teacher.Id] = teacher;
                Console.WriteLine($"Создан преподаватель {teacher.Name} | Id={teacher.Id}");
            }

            _service.AssignTeacherToCourse(courseId, teacher);
            Console.WriteLine($"Назначен преподаватель {teacher.Name} на курс {courseId}");
        }

        /// <summary>Записывает студента на курс.</summary>
        private static void EnrollStudent()
        {
            var courseId = ReadGuid("Id курса: ");

            Console.WriteLine("Выберите студента или создайте нового:");
            ListStudents();

            Console.Write("Введите Id существующего студента или пусто для создания: ");
            var input = Console.ReadLine();

            Student student;
            if (!string.IsNullOrWhiteSpace(input) && Guid.TryParse(input, out var studentId) && _students.TryGetValue(studentId, out var s))
            {
                student = s;
            }
            else
            {
                Console.Write("Имя студента: ");
                student = new Student(ReadNonEmpty());
                _students[student.Id] = student;
                Console.WriteLine($"Создан студент {student.Name} | Id={student.Id}");
            }

            _service.EnrollStudentToCourse(courseId, student);
            Console.WriteLine($"Студент {student.Name} записан на курс {courseId}");
        }

        /// <summary>Показывает все курсы конкретного преподавателя.</summary>
        private static void ShowTeacherCourses()
        {
            ListTeachers();
            var teacherId = ReadGuid("Id преподавателя: ");
            var courses = _service.GetCoursesByTeacher(teacherId);

            if (courses.Count == 0)
            {
                Console.WriteLine("Курсы не найдены.");
                return;
            }

            Console.WriteLine("Курсы преподавателя:");
            foreach (var c in courses)
                Console.WriteLine($"- {c.Title} | Id={c.Id} | Студентов={c.Students.Count}");
        }

        /// <summary>Показывает подробности курса: тип, назначенный преподаватель, список студентов.</summary>
        private static void ShowCourseDetails()
        {
            var id = ReadGuid("Id курса: ");
            var course = _service.GetCourse(id);

            if (course == null)
            {
                Console.WriteLine("Курс не найден.");
                return;
            }

            Console.WriteLine($"Курс: {course.Title} | Id={course.Id}");
            Console.WriteLine($"Тип: {GetCourseType(course)}");
            Console.WriteLine($"Преподаватель: {(course.Teacher is null ? "<не назначен>" : course.Teacher.Name)}");

            if (course is OnlineCourse oc)
                Console.WriteLine($"Платформа: {oc.Platform}, URL: {oc.Url}");
            if (course is OfflineCourse fc)
                Console.WriteLine($"Корпус: {fc.Building}, Аудитория: {fc.Room}");

            Console.WriteLine("Студенты:");
            if (course.Students.Count == 0) Console.WriteLine("  <нет>");
            foreach (var st in course.Students)
                Console.WriteLine($"  - {st.Name} | Id={st.Id}");
        }

        // ----------------------------- Вспомогательные методы -----------------------------

        private static string GetCourseType(ICourse c) =>
            c switch
            {
                OnlineCourse => "Онлайн",
                OfflineCourse => "Офлайн",
                _ => "Неизвестно"
            };

        private static string ReadNonEmpty()
        {
            while (true)
            {
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s))
                    return s.Trim();
                Console.Write("Пусто. Повторите: ");
            }
        }

        private static Guid ReadGuid(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                var s = Console.ReadLine();
                if (Guid.TryParse(s, out var id)) return id;
                Console.Write("Неверный Guid. Повторите: ");
            }
        }

        /// <summary>Быстрые демо-данные, чтобы сразу можно было что-то выбрать.</summary>
        private static void SeedDemoData()
        {
            var t = new Teacher("Иван Иванов");
            var s1 = new Student("Пётр Петров");
            var s2 = new Student("Анна Смирнова");

            _teachers[t.Id] = t;
            _students[s1.Id] = s1;
            _students[s2.Id] = s2;

            var c1 = _service.CreateOnlineCourse("C# Базовый", "Moodle", "https://example.com/csharp");
            var c2 = _service.CreateOfflineCourse("Алгоритмы", "Корпус А", "Ауд. 101");

            _service.AssignTeacherToCourse(c1.Id, t);
            _service.EnrollStudentToCourse(c1.Id, s1);
            _service.EnrollStudentToCourse(c1.Id, s2);

            // Показать стартовое состояние
            Console.WriteLine("Демо-данные созданы.");
            Console.WriteLine($"Преподаватель: {t.Name} | Id={t.Id}");
            Console.WriteLine($"Курс: {c1.Title} | Id={c1.Id} | Тип: Онлайн");
            Console.WriteLine($"Курс: {c2.Title} | Id={c2.Id} | Тип: Офлайн");
            Console.WriteLine($"Студенты: {s1.Name} | {s1.Id}; {s2.Name} | {s2.Id}");
            Console.WriteLine();
        }

        private static void ListTeachers()
        {
            if (_teachers.Count == 0)
            {
                Console.WriteLine("<Преподавателей нет>");
                return;
            }
            Console.WriteLine("Преподаватели:");
            foreach (var t in _teachers.Values)
                Console.WriteLine($"- {t.Name} | Id={t.Id}");
        }

        private static void ListStudents()
        {
            if (_students.Count == 0)
            {
                Console.WriteLine("<Студентов нет>");
                return;
            }
            Console.WriteLine("Студенты:");
            foreach (var s in _students.Values)
                Console.WriteLine($"- {s.Name} | Id={s.Id}");
        }
    }
}
