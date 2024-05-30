using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;

namespace Domain.Models;

public class Solution
{
    //Solution Model
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public int TutorId { get; set; }

    public int StudentId { get; set; }
    [DisplayName("Учень")] public string StudentName { get; set; } = string.Empty;
    [DisplayName("Статус")] public SolutionStatus Status { get; set; }

    [DisplayName("Відповідь")]
    [MaxLength(500)]
    public string? Answer { get; set; }

    [DisplayName("Коментар вчителя")]
    [MaxLength(500)]
    public string? TutorComment { get; set; }

    [DisplayName("Файли до завдання")] public List<UserFile> SolutionFiles { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    //Assignment Model
    [MaxLength(50)]
    [DisplayName("Вчитель")]

    public string TutorName { get; set; } = string.Empty;

    [DisplayName("Предмет")] public string SubjectName { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0,20}")]
    [MaxLength(50)]
    [DisplayName("Назва")]
    public string AssignmentTitle { get; set; } = string.Empty;

    [DisplayName("Опис завдання")]
    [StringLength(500, ErrorMessage = "{0} має містити принаймні {2} і не більше {1} символів.", MinimumLength = 0)]
    public string? Description { get; set; } = string.Empty;

    [DisplayName("Термін здачі")]
    public DateOnly Deadline { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(7));

    [DisplayName("Файли відповіді")] public List<UserFile> AssignmentFiles { get; set; } = [];
}