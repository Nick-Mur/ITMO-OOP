namespace LAB1.Courses.SpecificCourses;

using Objects;


public sealed class OnlineCourse(Teacher teacher, int courseId, string link): Course(teacher, courseId)
{
    public string Link { get; } = link;
    public override string GetCourseLocation() => $"Онлайн: {Link}";
}
