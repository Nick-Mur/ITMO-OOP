using University.Core.Models;
using University.Core.Courses;


namespace University.Core.Services
{
    /// <summary>
    /// Абстрактная базовая реализация сервиса управления курсами.
    /// Содержит общую in-memory реализацию.
    /// </summary>
    public abstract class CourseServiceBase : ICourseService
    {
        /// <summary>
        /// Хранилище курсов в памяти.
        /// </summary>
        protected readonly Dictionary<Guid, ICourse> Courses = new();

        /// <summary>
        /// Создаёт и регистрирует онлайн-курс.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <param name="platform">Платформа.</param>
        /// <param name="url">URL курса.</param>
        /// <returns>Созданный онлайн-курс.</returns>
        public virtual OnlineCourse CreateOnlineCourse(string title, string platform, string url)
        {
            var course = new OnlineCourse(title, platform, url);
            AddCourseInternal(course);
            return course;
        }

        /// <summary>
        /// Создаёт и регистрирует офлайн-курс.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <param name="building">Корпус.</param>
        /// <param name="room">Аудитория.</param>
        /// <returns>Созданный офлайн-курс.</returns>
        public virtual OfflineCourse CreateOfflineCourse(string title, string building, string room)
        {
            var course = new OfflineCourse(title, building, room);
            AddCourseInternal(course);
            return course;
        }

        /// <summary>
        /// Добавляет курс во внутреннее хранилище.
        /// </summary>
        /// <param name="course">Курс для добавления.</param>
        /// <exception cref="ArgumentNullException">Если курс равен null.</exception>
        protected void AddCourseInternal(ICourse course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            Courses.Add(course.Id, course);
        }

        /// <summary>
        /// Удаляет курс по идентификатору.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <returns>true, если курс был удалён; иначе false.</returns>
        public virtual bool RemoveCourse(Guid courseId)
        {
            return Courses.Remove(courseId);
        }

        /// <summary>
        /// Получает курс по идентификатору.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <returns>Курс или null, если не найден.</returns>
        public virtual ICourse GetCourse(Guid courseId)
        {
            Courses.TryGetValue(courseId, out var course);
            return course;
        }

        /// <summary>
        /// Назначает преподавателя на указанный курс.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <param name="teacher">Преподаватель.</param>
        /// <exception cref="InvalidOperationException">Если курс не найден.</exception>
        public virtual void AssignTeacherToCourse(Guid courseId, Teacher teacher)
        {
            var course = GetCourse(courseId) ?? throw new InvalidOperationException("Курс не найден.");
            course.AssignTeacher(teacher);
        }

        /// <summary>
        /// Записывает студента на указанный курс.
        /// </summary>
        /// <param name="courseId">Идентификатор курса.</param>
        /// <param name="student">Студент.</param>
        /// <exception cref="InvalidOperationException">Если курс не найден.</exception>
        public virtual void EnrollStudentToCourse(Guid courseId, Student student)
        {
            var course = GetCourse(courseId) ?? throw new InvalidOperationException("Курс не найден.");
            course.EnrollStudent(student);
        }

        /// <summary>
        /// Возвращает все курсы, которые ведёт заданный преподаватель.
        /// </summary>
        /// <param name="teacherId">Идентификатор преподавателя.</param>
        /// <returns>Список курсов преподавателя.</returns>
        public virtual IReadOnlyCollection<ICourse> GetCoursesByTeacher(Guid teacherId)
        {
            return Courses.Values
                .Where(c => c.Teacher != null && c.Teacher.Id == teacherId)
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Возвращает все доступные курсы.
        /// </summary>
        /// <returns>Список всех курсов.</returns>
        public virtual IReadOnlyCollection<ICourse> GetAllCourses()
        {
            return Courses.Values.ToList().AsReadOnly();
        }
    }
}
