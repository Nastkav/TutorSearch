using Web.Models.Shared;

namespace Web.Models.TutorProfile;

public class SearchViewModel
{
    public List<CheckboxViewModel> Subjects { get; set; } = [];
    public Dictionary<int, string> Cities { get; set; } = [];
    public List<TutorCardViewModel> TutorCards { get; set; } = [];
}