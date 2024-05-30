using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Review
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    [DisplayName("Вчитель")] public string TutorName { get; set; } = null!;

    [Range(1, 10, ErrorMessage = "Оцінка обов'язкова")]
    [DisplayName("Оцінка")]
    [Required(ErrorMessage = "Оцінка обов'язкова")]
    public int Rating { get; set; }

    [DisplayName("Опис")]
    [Required(ErrorMessage = "Введіть коментар")]
    [StringLength(1000, ErrorMessage = "{0} має містити не більше {1} символів.", MinimumLength = 0)]
    public string Description { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    [DisplayName("Автор")] public string AuthorName { get; set; } = null!;

    //ITrackable
    [DisplayName("Створено")] public DateTime CreatedAt { get; set; }
    [DisplayName("Оновлено")] public DateTime? UpdatedAt { get; set; }

    public string Avatar => "/avatars/" + AuthorId + ".png";
}