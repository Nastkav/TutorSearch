using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Profile;

public class ProfileVm
{
    [Required] [ReadOnly(true)] public int IdentityId { get; set; }
    public User UserVm { get; set; } = null!;
    public Tutor? TutorVm { get; set; }
    public List<SelectListItem> Cities { get; set; } = [];
    public List<SelectListItem> Subjects { get; set; } = [];
    public CreateRequestCommand CreateRequestCommand { get; set; } = new();
}