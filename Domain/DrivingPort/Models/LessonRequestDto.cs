namespace Domain.DrivingPort.Models;

public class LessonRequestDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string UserName { get; set; }
    public string TutorName { get; set; }
    public string UserComment { get; set; }
    public string TutorComment { get; set; }
    public bool? Answer { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}