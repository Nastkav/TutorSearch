using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Models;

namespace Domain.Models;

public class Solution
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    [MaxLength(50)] public string TaskTitle { get; set; } = string.Empty;

    public int StudentId { get; set; }
    public UserModel Student { get; set; } = null!;

    public SolutionStatus Status { get; set; }

    [MaxLength(500)] public string Answer { get; set; } = string.Empty;
    public virtual Dictionary<int, string> FileNames { get; set; } = [];

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}