using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class AssignmentModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorModel Tutor { get; set; } = null!;

    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; } = null!;
    [MaxLength(50)] public string Title { get; set; } = string.Empty;
    [MaxLength(500)] public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }

    public virtual List<FileModel> Files { get; set; } = [];
    public virtual List<SolutionModel> Solutions { get; set; } = [];

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}