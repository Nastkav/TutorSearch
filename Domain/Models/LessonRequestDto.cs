namespace Domain.Models;

public class LessonRequestDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string TutorName { get; set; } = string.Empty;
    public string TutorComment { get; set; } = string.Empty;
    public bool? Answer { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}