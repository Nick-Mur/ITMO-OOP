using University.Core.Models;

namespace University.Core.Courses
{
    /// <summary>
    /// Контракт для учебного курса.
    /// </summary>
    public interface ICourse
    {
        /// <summary>
        /// Уникальный идентификатор курса.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Название курса.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Назначенный преподаватель.
        /// Может быть null, если преподаватель не назначен.
        /// </summary>
        Teacher Teacher { get; }

        /// <summary>
        /// Список студентов, записанных на курс.
        /// </summary>
        IReadOnlyCollection<Student> Students { get; }

        /// <summary>
        /// Назначает преподавателя на курс.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        void AssignTeacher(Teacher teacher);

        /// <summary>
        /// Записывает студента на курс.
        /// </summary>
        /// <param name="student">Студент.</param>
        void EnrollStudent(Student student);

        /// <summary>
        /// Удаляет студента с курса по идентификатору.
        /// </summary>
        /// <param name="studentId">Идентификатор студента.</param>
        void RemoveStudent(Guid studentId);
    }
}