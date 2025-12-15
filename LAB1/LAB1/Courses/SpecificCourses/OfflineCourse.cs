namespace LAB1.Courses.SpecificCourses;

using Objects;


public sealed class OfflineCourse(Teacher teacher, int courseId, int audienceNumber): Course(teacher, courseId)
{
    public int AudienceNumber { get; } = audienceNumber;
    public override string GetCourseLocation() => $"Аудитория №{AudienceNumber}";
}
