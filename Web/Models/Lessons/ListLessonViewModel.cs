using Domain.Models;

namespace Web.Models.Lessons;

public class ListLessonViewModel
{
    public int UserId { get; set; }
    public bool IsTutor { get; set; }
    public List<Lesson> Lessons { get; set; } = [];
}