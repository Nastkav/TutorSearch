using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public enum SolutionStatus
{
    [Display(Name = "До виконання")] Todo,
    [Display(Name = "На Перевирці")] Review,
    [Display(Name = "Виконано")] Completed
}

public class SolutionModel : ITrackable
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public AssignmentModel Assignment { get; set; } = null!;

    public int StudentId { get; set; }
    public UserModel Student { get; set; } = null!;

    public SolutionStatus Status { get; set; }

    [MaxLength(500)] public string Answer { get; set; } = string.Empty;
    public virtual List<FileModel> Files { get; set; } = [];

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}