using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class RequestModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorProfile Tutor { get; set; } = null!;
    public CourseModel Course { get; set; } = null!;
    public CourseRequestStatus Status { get; set; }
    [ForeignKey("CreatedBy")] public UserModel Owner { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}