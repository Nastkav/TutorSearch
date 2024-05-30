using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.Models;

public class UserProfileModel
{
    [Display(Name = "Ім'я користувача")] public string? Username { get; set; }

    [Phone]
    [Display(Name = "Номер телефону")]
    public string? PhoneNumber { get; set; }
}