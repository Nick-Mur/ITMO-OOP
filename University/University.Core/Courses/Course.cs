using University.Core.Models;


namespace University.Core.Courses
{
    /// <summary>
    /// Абстрактная базовая реализация курса.
    /// Содержит общую логику работы со студентами и преподавателем.
    /// </summary>
    public abstract class Course : ICourse
    {
        private readonly List<Student> _students = new();

        /// <summary>
        /// Уникальный идентификатор курса.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название курса.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Назначенный преподаватель.
        /// Может быть null, если преподаватель не назначен.
        /// </summary>
        public Teacher Teacher { get; private set; }

        /// <summary>
        /// Список студентов, записанных на курс.
        /// </summary>
        public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

        /// <summary>
        /// Создаёт экземпляр базового курса.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <exception cref="ArgumentNullException">Если название равно null.</exception>
        protected Course(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Назначает преподавателя на курс.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <exception cref="ArgumentNullException">Если преподаватель равен null.</exception>
        public void AssignTeacher(Teacher teacher)
        {
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        /// <summary>
        /// Записывает студента на курс.
        /// </summary>
        /// <param name="student">Студент.</param>
        /// <exception cref="ArgumentNullException">Если студент равен null.</exception>
        /// <exception cref="InvalidOperationException">Если студент уже записан.</exception>
        public void EnrollStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (_students.Exists(s => s.Id == student.Id))
                throw new InvalidOperationException("Студент уже записан на курс.");

            _students.Add(student);
        }

        /// <summary>
        /// Удаляет студента с курса по идентификатору.
        /// </summary>
        /// <param name="studentId">Идентификатор студента.</param>
        /// <exception cref="InvalidOperationException">Если студент с таким Id не найден.</exception>
        public void RemoveStudent(Guid studentId)
        {
            var removed = _students.RemoveAll(s => s.Id == studentId);
            if (removed == 0)
                throw new InvalidOperationException("Студент с таким Id не найден на курсе.");
        }

        /// <summary>
        /// Возвращает строковое представление курса.
        /// </summary>
        /// <returns>Название и идентификатор курса.</returns>
        public override string ToString() => $"{Title} ({Id})";
    }
}
