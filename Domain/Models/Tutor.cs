using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Tutor
{
    [ReadOnly(true)] public int Id { get; set; }


    [Required]
    [Display(Name = "Про себе")]
    [StringLength(3000, ErrorMessage = "Введені дані не можуть бути довшими за 3000 символів")]
    [MinLength(50, ErrorMessage = "Занадто короткий текст")]
    public string About { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Домашня адреса")]
    [StringLength(300, ErrorMessage = "Введені дані не можуть бути довшими за 300 символів.")]
    public string Address { get; set; } = string.Empty;

    [DisplayName("У вчителя")] public bool TutorHomeAccess { get; set; }
    [DisplayName("В учня")] public bool StudentHomeAccess { get; set; }
    [DisplayName("Онлайн")] public bool OnlineAccess { get; set; }

    [DisplayName("Коротний опис")]
    [Required()]
    [StringLength(300, ErrorMessage = "Введені дані не можуть бути довшими за 300 символів.")]
    public string Descriptions { get; set; } = string.Empty;

    [MaxLength(400)] public string ImgPath { get; set; } = string.Empty;

    [Required()]
    [DisplayName("Ціна за одну годину")]
    [DataType(DataType.Currency)]
    [Range(10, 9999)]
    public decimal HourRate { get; set; }

    //-
    public Dictionary<int, string> Subjects { get; set; } = [];

    public int ReviewCount { get; set; }
    public float ReviewValue { get; set; }
}