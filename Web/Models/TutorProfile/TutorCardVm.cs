using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.TutorProfile;

public class TutorCardVm
{
    public User UserData { get; set; }
    public Tutor TutorData { get; set; }
    public Dictionary<int, string> Subjects { get; set; } = [];
}