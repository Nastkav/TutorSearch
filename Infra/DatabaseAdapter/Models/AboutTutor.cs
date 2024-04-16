using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Models;

public class AboutTutor
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public TutorProfile Profile { get; set; } = null!;
    [MaxLength(5000)] public string Content { get; set; } = string.Empty;
}