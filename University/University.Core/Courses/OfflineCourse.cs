// OfflineCourse.cs
using System;

namespace University.Core.Courses
{
    /// <summary>
    /// Конкретная реализация офлайн-курса.
    /// </summary>
    public class OfflineCourse : Course
    {
        /// <summary>
        /// Корпус, где проходит курс.
        /// </summary>
        public string Building { get; }

        /// <summary>
        /// Аудитория, где проходит курс.
        /// </summary>
        public string Room { get; }

        /// <summary>
        /// Создаёт экземпляр офлайн-курса.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <param name="building">Корпус.</param>
        /// <param name="room">Аудитория.</param>
        /// <exception cref="ArgumentNullException">
        /// Если одно из значений равно null.
        /// </exception>
        public OfflineCourse(string title, string building, string room)
            : base(title)
        {
            Building = building ?? throw new ArgumentNullException(nameof(building));
            Room = room ?? throw new ArgumentNullException(nameof(room));
        }

        /// <summary>
        /// Возвращает строковое представление офлайн-курса.
        /// </summary>
        /// <returns>Описание курса, корпуса и аудитории.</returns>
        public override string ToString()
            => $"Офлайн-курс: {Title}, корпус: {Building}, аудитория: {Room}";
    }
}