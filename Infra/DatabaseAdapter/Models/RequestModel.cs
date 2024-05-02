using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class RequestModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorProfileModel Tutor { get; set; } = null!;
    public CourseModel? Course { get; set; }
    public CourseRequestStatus Status { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    [MaxLength(300)] public string Comment { get; set; } = string.Empty;
    [MaxLength(300)] public string TutorComment { get; set; } = string.Empty;

    public UserModel Created { get; set; } = null!;

    public SubjectModel Subject { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedId { get; set; }
}