namespace Tests;

using Xunit;
using LAB1;
using LAB1.Objects;
using LAB1.Courses;
using LAB1.Courses.SpecificCourses;

public class CourseSystemTests
{

    [Fact]
    public void AddCourseToSystem()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course = new OnlineCourse(teacher, 1, "https://meet.example/1");

        system.AddCourse(course);

        Assert.Single(system.Courses);
        Assert.Equal(course, system.GetCourseByCourseId(1));
    }

    [Fact]
    public void RemoveCourseRemovesCourseById()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course1 = new OnlineCourse(teacher, 1, "https://meet.example/1");
        OfflineCourse course2 = new OfflineCourse(teacher, 2, 101);

        system.AddCourses([course1, course2]);

        system.RemoveCourse(1);

        Assert.Single(system.Courses);
        Assert.Equal(course2, system.GetCourseByCourseId(2));
    }

    [Fact]
    public void RemoveCourseThrowsIfCourseNotFound()
    {
        CourseSystem system = new CourseSystem();

        Assert.Throws<InvalidOperationException>(() => system.RemoveCourse(999));
    }

    [Fact]
    public void GetCourseByCourseIdThrowsWhenNotExists()
    {
        CourseSystem system = new CourseSystem();

        Assert.Throws<InvalidOperationException>(() => system.GetCourseByCourseId(123));
    }
    
    [Fact]
    public void SetTeacherToCourseChangesTeacher()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher1 = new Teacher("Ivan", "Ivan", 1);
        Teacher teacher2 = new Teacher("Petr", "Petr", 2);
        OfflineCourse course = new OfflineCourse(teacher1, 1, 201);
        system.AddCourse(course);

        system.SetTeacherToCourse(teacher2, 1);

        Assert.Equal(teacher2, system.GetCourseByCourseId(1).Teacher);
        Assert.Equal(2, system.GetCourseByCourseId(1).Teacher.TeacherId);
    }
    

    [Fact]
    public void AddStudentToCourse()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course = new OnlineCourse(teacher, 1, "https://meet.example/1");
        Student student = new Student("Alex", "Alex", 1);
        system.AddCourse(course);

        system.AddStudentToCourse(student, 1);

        List<Student> students = system.GetStudentsByCourseId(1);
        Assert.Single(students);
        Assert.Equal(student, students[0]);
    }

    [Fact]
    public void AddStudentsToCourse()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course = new OnlineCourse(teacher, 1, "https://meet.example/1");
        system.AddCourse(course);

        Student s1 = new Student("Alex", "Alex", 1);
        Student s2 = new Student("Maria", "Maria", 2);
        Student s3 = new Student("John", "John", 3);

        system.AddStudentsToCourse([s1, s2, s3], 1);

        List<Student> students = system.GetStudentsByCourseId(1);
        Assert.Equal(3, students.Count);
        Assert.Contains(s1, students);
        Assert.Contains(s2, students);
        Assert.Contains(s3, students);
    }

    [Fact]
    public void RemoveStudentFromCourse()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OfflineCourse course = new OfflineCourse(teacher, 1, 101);
        system.AddCourse(course);

        Student s1 = new Student("Alex", "Alex", 1);
        Student s2 = new Student("Maria", "Maria", 2);
        system.AddStudentsToCourse([s1, s2], 1);

        system.RemoveStudentFromCourse(s1, 1);

        List<Student> students = system.GetStudentsByCourseId(1);
        Assert.Single(students);
        Assert.Equal(s2, students[0]);
    }

    [Fact]
    public void GetStudentFromCourse()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course = new OnlineCourse(teacher, 1, "https://meet.example/1");
        system.AddCourse(course);

        Student student = new Student("Alex", "Alex", 1);
        system.AddStudentToCourse(student, 1);

        Student found = system.GetStudentFromCourse(1, 1);

        Assert.Equal(student, found);
        Assert.Equal(1, found.StudentId);
    }

    [Fact]
    public void GetStudentFromCourseThrowsIfNotFound()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course = new OnlineCourse(teacher, 1, "https://meet.example/1");
        system.AddCourse(course);

        Assert.Throws<InvalidOperationException>(() => system.GetStudentFromCourse(999, 1));
    }

    [Fact]
    public void GetTeacherCourses()
    {
        CourseSystem system = new CourseSystem();

        Teacher ivan = new Teacher("Ivan", "Ivan", 1);
        Teacher petr = new Teacher("Petr", "Petr", 2);

        OnlineCourse c1 = new OnlineCourse(ivan, 1, "https://meet/1");
        OfflineCourse c2 = new OfflineCourse(ivan, 2, 101);
        OnlineCourse c3 = new OnlineCourse(petr, 3, "https://meet/3");

        system.AddCourses([c1, c2, c3]);

        List<Course> ivanCourses = system.GetTeacherCourses(1);

        Assert.Equal(2, ivanCourses.Count);
        Assert.Contains(c1, ivanCourses);
        Assert.Contains(c2, ivanCourses);
        Assert.DoesNotContain(c3, ivanCourses);
    }
    
    [Fact]
    public void GetCourseLocationByCourseId()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OnlineCourse course = new OnlineCourse(teacher, 1, "https://meet.example/abc");
        system.AddCourse(course);

        string location = system.GetCourseLocationByCourseId(1);

        Assert.Contains("https://meet.example/abc", location, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GetCourseLocationByCourseIdOfflineCourse()
    {
        CourseSystem system = new CourseSystem();
        Teacher teacher = new Teacher("Teacher", "Teacher", 1);
        OfflineCourse course = new OfflineCourse(teacher, 1, 777);
        system.AddCourse(course);

        string location = system.GetCourseLocationByCourseId(1);

        Assert.Contains("777", location, StringComparison.OrdinalIgnoreCase);
    }
}
