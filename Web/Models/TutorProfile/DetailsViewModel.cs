using Web.Models.Shared;

namespace Web.Models.TutorProfile;

public class DetailsViewModel
{
    public List<CheckboxViewModel> Subjects { get; set; } = [];
    public Dictionary<int, string> Cities { get; set; } = [];
    public TutorCardViewModel TutorCard { get; set; } = new();

    public bool OnlineAccess { get; set; }
    public bool AtHomeAccess { get; set; }

    public bool OffsiteAccess { get; set; }
    public string About { get; set; } = null!;
    public string? Address { get; set; }
}