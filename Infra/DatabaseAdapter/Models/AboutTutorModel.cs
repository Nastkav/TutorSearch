using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace Infra.DatabaseAdapter.Models;

[Table("AboutTutor")]
public class AboutTutorModel
{
    [Required] public int Id { get; set; }
    [MaxLength(5000)] public string Content { get; set; } = string.Empty;
}