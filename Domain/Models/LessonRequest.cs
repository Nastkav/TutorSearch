using System.ComponentModel;
using AutoMapper.Configuration.Annotations;
using Infra.DatabaseAdapter.Helpers;

namespace Domain.Models;

public class LessonRequest
{
    [DisplayName("#")] public int Id { get; set; }

    [DisplayName("Предмет")] public string Subject { get; set; } = null!;
    [DisplayName("Учень")] public string UserName { get; set; } = string.Empty;
    [DisplayName("Вчитель")] public string TutorName { get; set; } = string.Empty;
    [DisplayName("Коментар вчителя")] public string TutorComment { get; set; } = string.Empty;


    [DisplayName("Статус")] public CourseRequestStatus Status { get; set; }
    [DisplayName("Початок")] public DateTime From { get; set; }
    [DisplayName("Кінець")] public DateTime To { get; set; }

    public bool IsTutor { get; set; }
    public int TutorId { get; set; }
    public int CreatedId { get; set; }
}