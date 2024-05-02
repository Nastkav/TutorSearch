using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.TutorProfile;

public class UserVm
{
    [DisplayName("���������� ������ ���������")]
    public bool TutorProfileEnabled { get; set; }

    [Required]
    [Display(Name = "��'�")]
    [StringLength(100, ErrorMessage = "����� ������ ����� � ��� ��'�")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "�������")]
    [StringLength(100, ErrorMessage = "����� ������ ����� � ��� �������")]
    public string Surname { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "����� ������ ����� � ��� �� �������")]
    [Display(Name = "�� �������")]
    public string? Patronymic { get; set; } = string.Empty;

    [Required][Display(Name = "̳���")] public string CitytId { get; set; } = "0";
}