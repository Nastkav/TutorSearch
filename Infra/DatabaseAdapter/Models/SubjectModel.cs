using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Models;

public class SubjectModel
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    public virtual List<TutorModel> Profiles { get; set; } = [];
}