using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class SolutionModel : ITrackable
{
    public int Id { get; set; }
    public Guid CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;
    [MaxLength(300)] public string Answer { get; set; } = string.Empty;

    public virtual List<FileModel> Files { get; set; } = [];

    public UserModel Created { get; set; } = null!;

    public int TaskId { get; set; }

    public TaskModel Task { get; set; }

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedId { get; set; }
}