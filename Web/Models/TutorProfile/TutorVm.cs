using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.TutorProfile;

public class TutorVm
{
    [Required]
    [Display(Name = "��� ���� (�� 3000 �������)")]
    [StringLength(3000, ErrorMessage = "������ ��� �� ������ ���� ������� �� 3000 �������")]
    [MinLength(50, ErrorMessage = "������� �������� �����")]
    public string About { get; set; } = string.Empty;

    [Required]
    [StringLength(300, ErrorMessage = "������ ��� �� ������ ���� ������� �� 300 �������.")]
    public string Address { get; set; } = string.Empty;

    [DisplayName("� �������")] public bool TutorHomeAccess { get; set; }
    [DisplayName("� ����")] public bool StudentHomeAccess { get; set; }
    [DisplayName("������")] public bool OnlineAccess { get; set; }

    [DisplayName("�������� ����")]
    [Required()]
    [StringLength(300, ErrorMessage = "������ ��� �� ������ ���� ������� �� 300 �������.")]
    public string Descriptions { get; set; } = string.Empty;

    public string SubjectId { get; set; } = "0";

    public string ImgPath { get; set; } = string.Empty;

    [Required()]
    [DisplayName("ֳ�� �� ���� ������")]
    public decimal HourRate { get; set; }

    public string GetImgPath() => ImgPath != string.Empty ? ImgPath : "/img/example_face.jpg";
}