using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class TutorProfile : ITrackable
{
    public int Id { get; set; }

    public AboutTutor About { get; set; } = null!;
    public virtual List<SubjectModel> Subjects { get; set; } = [];
    public virtual List<AvailableTime> AvailableTimes { get; set; } = [];
    public virtual List<CourseModel> Courses { get; set; } = [];
    public virtual List<LessonModel> TeachingLessons { get; set; } = [];

    [ForeignKey("CreatedBy")] public UserModel Owner { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}