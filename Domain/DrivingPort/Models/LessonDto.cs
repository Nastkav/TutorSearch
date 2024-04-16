using Domain.Port.Driving;

namespace Domain.DrivingPort.Models;

public class LessonDto
{
    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public class LessonDetailsDto : LessonDto
{
    public string Subject { get; set; } = null!;
    public string Comment { get; set; } = "";

    public Guid CourseId { get; set; }
    public string Course { get; set; } = null!;
}