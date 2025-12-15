namespace LAB1;

using Objects;
using Courses;
    

public class CourseSystem
{
    public List<Course> Courses { get; private set; } = new();
    
    public void AddCourse(Course course) => Courses.Add(course);
    public void AddCourses(IEnumerable<Course> courses) => Courses.AddRange(courses);
    public void RemoveCourse(int courseId) => Courses.Remove(GetCourseByCourseId(courseId));

    public void RemoveCourses(IEnumerable<int> courseIds)
    {
        foreach (var courseId in courseIds)
            Courses.Remove(GetCourseByCourseId(courseId));
    }

    public Course GetCourseByCourseId(int courseId)
    {
        Course? course = Courses.FirstOrDefault(c => c.CourseId == courseId);

        return course ?? throw new InvalidOperationException($"Курс '{courseId}' не найден.");
    }
    
    public void SetTeacherToCourse(Teacher teacher,  int courseId) => GetCourseByCourseId(courseId).SetTeacherToCourse(teacher);
    public void AddStudentToCourse(Student student, int courseId) => GetCourseByCourseId(courseId).AddStudent(student);
    public void AddStudentsToCourse(IEnumerable<Student> students, int courseId) => GetCourseByCourseId(courseId).AddStudents(students);
    public void RemoveStudentFromCourse(Student student, int courseId) => GetCourseByCourseId(courseId).RemoveStudent(student);
    public void RemoveStudentsFromCourse(IEnumerable<Student> students, int courseId) =>  GetCourseByCourseId(courseId).RemoveStudents(students);
    public List<Student> GetStudentsByCourseId(int courseId) =>  GetCourseByCourseId(courseId).Students;
    public Student GetStudentFromCourse(int studentId, int courseId) => GetCourseByCourseId(courseId).GetStudentById(studentId);

    public List<Course> GetTeacherCourses(int teacherId) => Courses.Where(c => c.Teacher.TeacherId == teacherId).ToList();

    
    public string GetCourseLocationByCourseId(int courseId) => GetCourseByCourseId(courseId).GetCourseLocation();
}
