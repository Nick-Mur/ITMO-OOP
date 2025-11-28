// Student.cs
namespace University.Core.Models
{
    /// <summary>
    /// Конкретная реализация студента.
    /// </summary>
    public class Student : Person
    {
        /// <summary>
        /// Создаёт экземпляр студента.
        /// </summary>
        /// <param name="name">Имя студента.</param>
        public Student(string name) : base(name)
        {
        }
    }
}