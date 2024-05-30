using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }

    [DisplayName("Активувати профіль викладача")]
    public bool ProfileEnabled { get; set; }

    [Required(ErrorMessage = "Поле '{0}' є обов'язковим")]
    [Display(Name = "Ім'я")]
    [StringLength(50, ErrorMessage = "Надто довгий текст у полі ім'я", MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    public string Avatar => "/avatars/" + Id + ".png";

    [Required(ErrorMessage = "Поле '{0}' є обов'язковим")]
    [Display(Name = "Прізвище")]
    [StringLength(50, ErrorMessage = "Надто довгий текст у полі прізвище", MinimumLength = 3)]
    public string Surname { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "Надто довгий текст у полі по батькові")]
    [Required(ErrorMessage = "Поле '{0}' є обов'язковим")]
    [Display(Name = "По батькові")]
    public string Patronymic { get; set; } = string.Empty;

    [Required(ErrorMessage = "Поле '{0}' є обов'язковим")]
    [Display(Name = "Місто")]
    public string CityId { get; set; } = "0";

    public string FullName { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
}