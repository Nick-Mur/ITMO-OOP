// Teacher.cs
namespace University.Core.Models
{
    /// <summary>
    /// Конкретная реализация преподавателя.
    /// </summary>
    public class Teacher : Person
    {
        /// <summary>
        /// Создаёт экземпляр преподавателя.
        /// </summary>
        /// <param name="name">Имя преподавателя.</param>
        public Teacher(string name) : base(name)
        {
        }
    }
}