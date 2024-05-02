using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class TaskModel : ITrackable
{
    public int Id { get; set; }
    public Guid CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;

    public virtual List<FileModel> Files { get; set; } = [];
    public virtual List<SolutionModel> Solutions { get; set; } = [];
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedId { get; set; }
}