namespace LAB1.Objects;

public class Teacher(string name, string surname, int teacherId)
{
    public string Name { get; private set; } = name;
    public string Surname { get; private set; } = surname;
    public int TeacherId { get; private set; } =  teacherId;
}