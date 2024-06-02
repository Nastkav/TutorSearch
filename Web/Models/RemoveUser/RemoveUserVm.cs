using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.RemoveUser;

public class RemoveUserVm
{
    [Display(Name = "Користувач")] public string UserId { get; set; } = null!;
    public List<SelectListItem> UserList { get; set; } = new();
}