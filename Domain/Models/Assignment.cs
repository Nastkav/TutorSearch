using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class Assignment
{
    [DisplayName("#")] public int Id { get; set; }
    [Range(1, int.MaxValue)] public int TutorId { get; set; }

    [DisplayName("Вчитель")] public string TutorName { get; set; } = string.Empty;

    [DisplayName("Предмет")]
    [Range(1, int.MaxValue, ErrorMessage = "Оберіть предмет")]
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; } = null;

    [DisplayName("Назва завдання")]
    [StringLength(50, ErrorMessage = "{0} має містити принаймні {2} і не більше {1} символів.", MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [DisplayName("Опис завдання")]
    [StringLength(500, ErrorMessage = "{0} має містити принаймні {2} і не більше {1} символів.", MinimumLength = 0)]
    public string? Description { get; set; } = string.Empty;

    [DisplayName("Термін сдачі")]
    public DateOnly Deadline { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(7));


    /// <summary>
    /// string - File name
    /// string - File path
    /// </summary>
    [DisplayName("Додані файли")]
    public List<UserFile> FileNames { get; set; } = [];


    [DisplayName("Учні")] public List<int> StudentsIds { get; set; } = [];

    [DisplayName("Учні")] public string StudentNames { get; set; } = string.Empty;
    [DisplayName("Створено")] public DateTime CreatedAt { get; set; }
    [DisplayName("Оновлено")] public DateTime? UpdatedAt { get; set; }
}