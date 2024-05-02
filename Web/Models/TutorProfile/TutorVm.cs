using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.TutorProfile;

public class TutorVm
{
    [Required]
    [Display(Name = "Про себе (до 3000 символів)")]
    [StringLength(3000, ErrorMessage = "Введені дані не можуть бути довшими за 3000 символів")]
    [MinLength(50, ErrorMessage = "Занадто короткий текст")]
    public string About { get; set; } = string.Empty;

    [Required]
    [StringLength(300, ErrorMessage = "Введені дані не можуть бути довшими за 300 символів.")]
    public string Address { get; set; } = string.Empty;

    [DisplayName("У вчителя")] public bool TutorHomeAccess { get; set; }
    [DisplayName("В учня")] public bool StudentHomeAccess { get; set; }
    [DisplayName("Онлайн")] public bool OnlineAccess { get; set; }

    [DisplayName("Коротний опис")]
    [Required()]
    [StringLength(300, ErrorMessage = "Введені дані не можуть бути довшими за 300 символів.")]
    public string Descriptions { get; set; } = string.Empty;

    public string SubjectId { get; set; } = "0";

    public string ImgPath { get; set; } = string.Empty;

    [Required()]
    [DisplayName("Ціна за одну годину")]
    public decimal HourRate { get; set; }

    public string GetImgPath() => ImgPath != string.Empty ? ImgPath : "/img/example_face.jpg";
}