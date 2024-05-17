using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Review
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    [DisplayName("Вчиталь")] public string TutorName { get; set; } = null!;
    [Range(0, 5)] [DisplayName("Оцінка")] public int Rating { get; set; }

    [MaxLength(300)]
    [DisplayName("Коментар")]
    public string Description { get; set; } = string.Empty;

    public int CreatedId { get; set; }
    [DisplayName("Автор")] public string Created { get; set; } = null!;

    //ITrackable
    [DisplayName("Створено")] public DateTime CreatedAt { get; set; }
    [DisplayName("Оновлено")] public DateTime? UpdatedAt { get; set; }
}