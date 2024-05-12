using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.Models;

public class UserProfileModel
{
    public string? Username { get; set; }

    [Phone]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }
}