using Domain.Port.Driving;

namespace Domain.Models;

public class LessonDto
{
    public int Id { get; set; }
    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

public class LessonDetailsDto : LessonDto
{
    public string? Subject { get; set; }
    public string? Comment { get; set; }
    public string? CourseId { get; set; }
    public string? CourseName { get; set; }
}