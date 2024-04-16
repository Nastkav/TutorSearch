using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class LessonModel : ITrackable
{
    public int Id { get; set; }
    public int TutorProfileId { get; set; }
    public TutorProfile TutorProfile { get; set; } = null!;
    public CourseModel Course { get; set; } = null!;
    public virtual List<UserModel> Students { get; set; } = [];
    public virtual List<Attendance> Attendances { get; set; } = [];


    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}