using System.ComponentModel;

namespace Domain.Models;

public class LessonRequestDto
{
    [DisplayName("#")] public int Id { get; set; }
    [DisplayName("�������")] public string Subject { get; set; } = null!;
    [DisplayName("�����")] public string UserName { get; set; } = string.Empty;
    [DisplayName("�������")] public string TutorName { get; set; } = string.Empty;
    [DisplayName("�������� �������")] public string TutorComment { get; set; } = string.Empty;

    public bool IsTutor { get; set; }

    [DisplayName("³������")] public bool? Answer { get; set; }
    [DisplayName("�������")] public DateTime From { get; set; }
    [DisplayName("ʳ����")] public DateTime To { get; set; }
}