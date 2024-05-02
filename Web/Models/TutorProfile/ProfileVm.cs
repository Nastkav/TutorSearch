using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.TutorProfile;

public class ProfileVm
{
    [Required] public int ProfileId { get; set; }
    public UserVm UserVm { get; set; } = new();
    public TutorVm? TutorVm { get; set; }
    public List<SelectListItem> Cities { get; set; } = [];
    public List<SelectListItem> Subjects { get; set; } = [];
}