using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Profile;

public class ProfileVm : TutorCardVm
{
    [Required] public int IdentityId { get; set; }
    public List<SelectListItem> Cities { get; set; } = [];

    public CreateRequestCommand CreateRequestCommand { get; set; } = new();

    [DisplayName("Завантажити новий аватар")]
    public IFormFile? NewAvatar { get; set; }

    public List<Review> Reviews { get; set; } = [];
}