using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class FileModel : ITrackable
{
    public int Id { get; set; }
    public int? TaskId { get; set; }
    public TaskModel? Task { get; set; }
    public int? SolutionId { get; set; }
    public SolutionModel? Solution { get; set; }

    public int OwnerId { get; set; }
    public UserModel Owner { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}