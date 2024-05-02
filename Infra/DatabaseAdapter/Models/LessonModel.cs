using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class LessonModel : ITrackable
{
    public int Id { get; set; }
    public int TutorProfileId { get; set; }
    public TutorProfileModel TutorProfile { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    [MaxLength(200)] public string Comment { get; set; } = string.Empty;

    public Guid CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;
    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; } = null!;

    public virtual List<UserModel> Students { get; set; } = [];
    public virtual List<Attendance> Attendances { get; set; } = [];


    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedId { get; set; }
}