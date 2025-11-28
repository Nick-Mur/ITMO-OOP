// OnlineCourse.cs
using System;

namespace University.Core.Courses
{
    /// <summary>
    /// Конкретная реализация онлайн-курса.
    /// </summary>
    public class OnlineCourse : Course
    {
        /// <summary>
        /// Название обучающей платформы.
        /// </summary>
        public string Platform { get; }

        /// <summary>
        /// Ссылка на курс.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Создаёт экземпляр онлайн-курса.
        /// </summary>
        /// <param name="title">Название курса.</param>
        /// <param name="platform">Платформа.</param>
        /// <param name="url">URL курса.</param>
        /// <exception cref="ArgumentNullException">
        /// Если одно из значений равно null.
        /// </exception>
        public OnlineCourse(string title, string platform, string url)
            : base(title)
        {
            Platform = platform ?? throw new ArgumentNullException(nameof(platform));
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        /// <summary>
        /// Возвращает строковое представление онлайн-курса.
        /// </summary>
        /// <returns>Описание курса, платформы и URL.</returns>
        public override string ToString()
            => $"Онлайн-курс: {Title}, платформа: {Platform}, URL: {Url}";
    }
}