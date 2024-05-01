using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Models;

public class CourseModel
{
    [Key] public Guid Id { get; set; }

    [MaxLength(50), Required] public string Title { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; } = null!;
    public int RequestId { get; set; }
    public RequestModel Request { get; set; } = null!;

    public int TutorId { get; set; }
    public TutorProfileModel Tutor { get; set; } = null!;

    public virtual List<CourseReviewModel> Reviews { get; set; } = [];

    public virtual List<LessonModel> Lessons { get; set; } = [];
    public virtual List<UserModel> Students { get; set; } = [];
    public virtual List<TaskModel> Tasks { get; set; } = [];
}