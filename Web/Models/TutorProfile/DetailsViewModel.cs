using Domain.Commands;
using Web.Models.Shared;

namespace Web.Models.TutorProfile;

public class DetailsViewModel
{
    public List<CheckboxViewModel> Subjects { get; set; } = [];

    public TutorCardViewModel TutorCard { get; set; } = new();
    public CreateRequestCommand RequestCommand { get; set; } = new();
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }

    public bool OnlineAccess { get; set; }
    public string About { get; set; } = null!;
    public string? Address { get; set; }
    public int CurrentUserId { get; set; }
}
//TODO: To settings public Dictionary<int, string> Cities { get; set; } = [];