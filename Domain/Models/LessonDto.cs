using Domain.Port.Driving;

namespace Domain.Models;

public class LessonDto
{
    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public class LessonDetailsDto : LessonDto
{
    public string? Subject { get; set; }
    public string? Comment { get; set; }
    public string? CourseId { get; set; }
    public string? CourseName { get; set; }
}