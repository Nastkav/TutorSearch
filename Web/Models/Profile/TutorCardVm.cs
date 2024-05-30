using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Profile;

public class TutorCardVm
{
    public User UserVm { get; set; } = null!;
    public Tutor? TutorVm { get; set; } = null!;
    public List<SelectListItem> Subjects { get; set; } = [];
}