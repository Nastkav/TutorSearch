using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class FileModel : ITrackable
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = string.Empty;
    [MaxLength(300)] public string Path { get; set; } = string.Empty;

    public int OwnerId { get; set; }
    public UserModel Owner { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual List<AssignmentModel> Assignments { get; set; } = [];
    public virtual List<SolutionModel> Solutions { get; set; } = [];
}