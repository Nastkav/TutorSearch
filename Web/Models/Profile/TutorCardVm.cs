using Domain.Models;

namespace Web.Models.Profile;

public class TutorCardVm
{
    public User UserData { get; set; } = null!;
    public Tutor TutorData { get; set; } = null!;
    public Dictionary<int, string> Subjects { get; set; } = [];
}