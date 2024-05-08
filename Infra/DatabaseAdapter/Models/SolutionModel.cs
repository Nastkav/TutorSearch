using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class SolutionModel : ITrackable
{
    public int Id { get; set; }

    public int TaskId { get; set; }
    public TaskModel Task { get; set; } = null!;

    public int StudentId { get; set; }
    public UserModel Student { get; set; } = null!;


    [MaxLength(500)] public string Answer { get; set; } = string.Empty;
    public virtual List<FileModel> Files { get; set; } = [];

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}