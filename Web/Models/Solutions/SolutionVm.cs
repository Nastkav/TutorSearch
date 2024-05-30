using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Solutions;

public class SolutionVm
{
    public int? UserId { get; set; }
    public Solution Solution { get; set; } = new();
    public List<SelectListItem> Subjects { get; set; } = [];
    public bool IsTutor { get; set; }
}