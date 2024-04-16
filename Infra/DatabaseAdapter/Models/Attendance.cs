namespace Infra.DatabaseAdapter.Models;

public class Attendance
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public UserModel Student { get; set; } = null!;
    public int LessonId { get; set; }
    public LessonModel Lesson { get; set; } = null!;
}