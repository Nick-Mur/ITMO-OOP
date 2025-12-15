namespace LAB1.Objects;

public class Student(string name, string surname, int studentId)
{
    public string Name { get; private set; } = name;
    public string Surname { get; private set; } = surname;
    public int StudentId { get; private set; } =  studentId;
}