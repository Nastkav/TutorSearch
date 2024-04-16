using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Models;

public class SubjectModel
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    public virtual List<TutorProfile> Profiles { get; set; } = [];
    public virtual List<CourseModel> Courses { get; set; } = [];
}