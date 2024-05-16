using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }

    [DisplayName("Активувати профль викладача")]
    public bool ProfileEnabled { get; set; }

    [Required]
    [Display(Name = "Ім'я")]
    [StringLength(50, ErrorMessage = "Надто довгий текст у полі ім'я")]
    public string Name { get; set; } = string.Empty;

    public string Avatar => "/avatars/" + Id + ".png";

    [Required]
    [Display(Name = "Прізвище")]
    [StringLength(50, ErrorMessage = "Надто довгий текст у полі прізвище")]
    public string Surname { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "Надто довгий текст у полі по батькові")]
    [MinLength(0)]
    [Display(Name = "По батькові")]
    public string Patronymic { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Оберіть місто")]
    [Display(Name = "Місто")]
    public string CityId { get; set; } = "0";

    public string FullName { get; set; } = string.Empty;
}