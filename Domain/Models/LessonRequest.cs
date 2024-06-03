using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using AutoMapper.Configuration.Annotations;
using Infra.DatabaseAdapter.Helpers;

namespace Domain.Models;

public class LessonRequest
{
    [DisplayName("#")] public int Id { get; set; }

    [DisplayName("Предмет")] public string Subject { get; set; } = null!;
    [DisplayName("Учень")] public string UserName { get; set; } = string.Empty;
    [DisplayName("Вчитель")] public string TutorName { get; set; } = string.Empty;

    [StringLength(254, ErrorMessage = "{0} має містити не більше {1} символів.", MinimumLength = 0)]
    [DisplayName("Посилання на заняття")] public string TutorComment { get; set; } = string.Empty;
    [DisplayName("Коментар учня")] public string Comment { get; set; } = string.Empty;


    [DisplayName("Статус")] public LessonRequestStatus Status { get; set; }

    [DisplayName("Початок")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}")]
    public DateTime From { get; set; }


    [DisplayName("Кінець")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}")]

    public DateTime To { get; set; }

    public bool IsTutor { get; set; }
    public int TutorId { get; set; }
    public int CreatedId { get; set; }


    [DisplayName("Дата")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateOnly LessonDate => DateOnly.FromDateTime(From);
}