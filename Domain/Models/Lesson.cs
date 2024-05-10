using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Models;

public class Lesson
{
    public int Id { get; set; }
    [Range(1, int.MaxValue)] public int TutorId { get; set; }

    [ReadOnly(true)]
    [DisplayName("Вчитель")]
    public string TutorName { get; set; } = string.Empty;

    [DisplayName("Початок")] public DateTime From { get; set; }
    [DisplayName("Кінець")] public DateTime To { get; set; }

    [StringLength(254, ErrorMessage = "{0} має містити не більше {1} символів.")]
    [DisplayName("Коментар")]
    public string? Comment { get; set; } = string.Empty;

    [DisplayName("Предмет")]
    [Range(1, int.MaxValue, ErrorMessage = "Оберіть предмет")]
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; } = null;

    /// <summary>
    /// Id,Name
    /// </summary>
    [DisplayName("Учні")]
    public virtual Dictionary<int, string> Students { get; set; } = [];

    public virtual List<int> StudentsIds { get; set; } = [];

    [DisplayName("Учні")] public string StudentNames => string.Join(", ", Students.Values);
}