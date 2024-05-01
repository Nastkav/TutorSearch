using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Models;

public class AboutTutorModel
{
    [Required] public int Id { get; set; }
    [MaxLength(5000)] public string Content { get; set; } = string.Empty;
}