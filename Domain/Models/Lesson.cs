using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Models;

public class Lesson
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    [ReadOnly(true)] public string TutorName { get; set; } = string.Empty;

    public DateTime From { get; set; }
    public DateTime To { get; set; }

    [MaxLength(200)] public string Comment { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = null!;

    /// <summary>
    /// Id,Name
    /// </summary>
    public virtual Dictionary<int, string> Students { get; set; } = [];

    public virtual List<int> StudentsIds { get; set; } = [];

    [DisplayName("Студенти")] public string StudentNames => string.Join(", ", Students.Values);
}