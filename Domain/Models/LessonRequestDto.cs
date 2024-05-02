using System.ComponentModel;

namespace Domain.Models;

public class LessonRequestDto
{
    [DisplayName("#")] public int Id { get; set; }
    [DisplayName("Предмет")] public string Subject { get; set; } = null!;
    [DisplayName("Учень")] public string UserName { get; set; } = string.Empty;
    [DisplayName("Вчитель")] public string TutorName { get; set; } = string.Empty;
    [DisplayName("Коментар вчителя")] public string TutorComment { get; set; } = string.Empty;

    public bool IsTutor { get; set; }

    [DisplayName("Відповідь")] public bool? Answer { get; set; }
    [DisplayName("Початок")] public DateTime From { get; set; }
    [DisplayName("Кінець")] public DateTime To { get; set; }
}