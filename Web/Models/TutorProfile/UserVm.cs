using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.TutorProfile;

public class UserVm
{
    [DisplayName("Активувати профль викладача")]
    public bool TutorProfileEnabled { get; set; }

    [Required]
    [Display(Name = "Ім'я")]
    [StringLength(100, ErrorMessage = "Надто довгий текст у полі ім'я")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Прізвище")]
    [StringLength(100, ErrorMessage = "Надто довгий текст у полі прізвище")]
    public string Surname { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Надто довгий текст у полі по батькові")]
    [Display(Name = "По батькові")]
    public string? Patronymic { get; set; } = string.Empty;

    [Required][Display(Name = "Місто")] public string CitytId { get; set; } = "0";
}