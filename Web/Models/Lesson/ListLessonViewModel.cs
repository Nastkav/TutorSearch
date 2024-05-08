namespace Web.Models.Lesson;

public class ListLessonViewModel
{
    public int UserId { get; set; }
    public bool IsTutor { get; set; }
    public List<Domain.Models.Lesson> Lessons { get; set; } = [];
}