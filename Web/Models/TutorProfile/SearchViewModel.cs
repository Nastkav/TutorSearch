using System.Drawing.Printing;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Shared;

namespace Web.Models.TutorProfile;

public class SearchViewModel
{
    public List<SelectListItem> Subjects { get; set; } = [];
    public List<SelectListItem> Cities { get; set; } = [];
    public List<TutorCardViewModel> TutorCards { get; set; } = [];
    public GetTutorsQuery Filters { get; set; } = new();
}