namespace University.Core.Models
{
    /// <summary>
    /// Базовый абстрактный класс для всех участников университета.
    /// Содержит идентификатор и имя.
    /// </summary>
    public abstract class Person
    {
        /// <summary>
        /// Уникальный идентификатор персоны.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Имя персоны.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Создаёт экземпляр персоны.
        /// </summary>
        /// <param name="name">Имя персоны.</param>
        /// <exception cref="ArgumentNullException">Если имя равно null.</exception>
        protected Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Возвращает строковое представление персоны.
        /// </summary>
        /// <returns>Имя и идентификатор.</returns>
        public override string ToString() => $"{Name} ({Id})";
    }
}