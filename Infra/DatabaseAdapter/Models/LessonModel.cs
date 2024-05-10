using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class LessonModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorModel Tutor { get; set; } = null!;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    [MaxLength(50)] public string Title { get; set; } = string.Empty;
    [MaxLength(200)] public string Comment { get; set; } = string.Empty;

    public int? RequestId { get; set; }
    public RequestModel? Request { get; set; } = null;
    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; } = null!;

    public virtual List<UserModel> Students { get; set; } = [];


    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}