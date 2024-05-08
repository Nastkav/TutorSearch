using Domain.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Profile;

public class SearchVm
{
    public List<SelectListItem> Subjects { get; set; } = [];
    public List<SelectListItem> Cities { get; set; } = [];
    public List<TutorCardVm> TutorCards { get; set; } = [];
    public GetTutorsQuery Filters { get; set; } = new();
}