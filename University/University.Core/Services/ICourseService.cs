using University.Core.Models;
using University.Core.Courses;


namespace University.Core.Services
{
    /// <summary>
    /// Контракт сервиса управления курсами.
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Создаёт и регистрирует онлайн-курс.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <param name="platform">Платформа.</param>
        /// <param name="url">URL курса.</param>
        /// <returns>Созданный онлайн-курс.</returns>
        OnlineCourse CreateOnlineCourse(string title, string platform, string url);

        /// <summary>
        /// Создаёт и регистрирует офлайн-курс.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <param name="building">Корпус.</param>
        /// <param name="room">Аудитория.</param>
        /// <returns>Созданный офлайн-курс.</returns>
        OfflineCourse CreateOfflineCourse(string title, string building, string room);

        /// <summary>
        /// Удаляет курс по идентификатору.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <returns>true, если курс был удалён; иначе false.</returns>
        bool RemoveCourse(Guid courseId);

        /// <summary>
        /// Получает курс по идентификатору.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <returns>Курс или null, если не найден.</returns>
        ICourse GetCourse(Guid courseId);

        /// <summary>
        /// Назначает преподавателя на указанный курс.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <param name="teacher">Преподаватель.</param>
        void AssignTeacherToCourse(Guid courseId, Teacher teacher);

        /// <summary>
        /// Записывает студента на указанный курс.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <param name="student">Студент.</param>
        void EnrollStudentToCourse(Guid courseId, Student student);

        /// <summary>
        /// Возвращает все курсы, которые ведёт заданный преподаватель.
        /// </summary>
        /// <param name="teacherId">Идентификатор преподавателя.</param>
        /// <returns>Список курсов преподавателя.</returns>
        IReadOnlyCollection<ICourse> GetCoursesByTeacher(Guid teacherId);

        /// <summary>
        /// Возвращает все доступные курсы.
        /// </summary>
        /// <returns>Список всех курсов.</returns>
        IReadOnlyCollection<ICourse> GetAllCourses();
    }
}
