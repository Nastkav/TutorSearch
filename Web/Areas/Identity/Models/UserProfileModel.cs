using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.Models;

public class UserProfileModel
{
    public string? Username { get; set; }

    [Phone]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Name")]
    public string? Name { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Surname")]
    public string? Surname { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Patronymic")]
    public string? Patronymic { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Birth Date")]
    public DateTime? BirthDate { get; set; }
}