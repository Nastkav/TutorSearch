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
    [ForeignKey("CreatedBy")] public UserModel Owner { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}