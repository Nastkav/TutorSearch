using Domain.Port.Driving;

namespace Domain.Models;

public class LessonEvent
{
    public int Id { get; set; }
    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}