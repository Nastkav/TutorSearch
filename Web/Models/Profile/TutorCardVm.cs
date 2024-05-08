using Domain.Models;

namespace Web.Models.Profile;

public class TutorCardVm
{
    public User UserData { get; set; }
    public Tutor TutorData { get; set; }
    public Dictionary<int, string> Subjects { get; set; } = [];
}