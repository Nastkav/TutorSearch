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

    [DisplayName("Предмет")] public string? SubjectName { get; set; } = null;

    [DisplayName("Назва завдання")]
    [StringLength(50, ErrorMessage = "{0} має містити принаймні {2} і не більше {1} символів.", MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [DisplayName("Опис завдання")]
    [StringLength(500, ErrorMessage = "{0} має містити принаймні {2} і не більше {1} символів.", MinimumLength = 0)]
    public string? Description { get; set; } = string.Empty;

    [DisplayName("Термін здачі")] public DateTime Deadline { get; set; } = DateTime.Today.AddDays(7).Date;


    /// <summary>
    /// string - File name
    /// string - File path
    /// </summary>
    [DisplayName("Файли до завдання")]
    public List<UserFile> FileNames { get; set; } = [];


    [DisplayName("Учні")] public List<int> StudentsIds { get; set; } = [];

    [DisplayName("Учні")] public string StudentNames { get; set; } = string.Empty;
    [DisplayName("Додано")] public DateTime CreatedAt { get; set; }
    [DisplayName("Оновлено")] public DateTime? UpdatedAt { get; set; }
}