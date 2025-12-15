namespace LAB1.Courses;

using Objects;

public abstract class Course(Teacher teacher, int courseId)
{
    public List<Student> Students { get; private set; } = new();
    
    public Teacher Teacher { get; private set; } = teacher;
    public int CourseId { get; private set; } = courseId;
    
    public void AddStudent(Student student) => Students.Add(student);
    public void AddStudents(IEnumerable<Student> students) => Students.AddRange(students);
    public void RemoveStudent(Student student) => Students.Remove(student);

    public void RemoveStudents(IEnumerable<Student> students)
    {
        foreach (Student student in students)
        {
            Students.Remove(student);
        }
    }
    
    public Student GetStudentById(int studentId)
    {
        Student? student =
            Students.FirstOrDefault(s => s.StudentId == studentId);

        return student ?? throw new InvalidOperationException($"Студент с ID '{studentId}' не найден.");
    }
    
    public void SetTeacherToCourse(Teacher teacher) => Teacher = teacher;
    
    public abstract string GetCourseLocation();
}