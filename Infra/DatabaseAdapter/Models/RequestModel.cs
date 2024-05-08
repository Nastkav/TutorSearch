using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class RequestModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorModel Tutor { get; set; } = null!;
    public LessonRequestStatus Status { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    [MaxLength(300)] public string Comment { get; set; } = string.Empty;
    [MaxLength(300)] public string TutorComment { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; } = null!;
    public int CreatedId { get; set; }
    public UserModel Created { get; set; } = null!;


    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}