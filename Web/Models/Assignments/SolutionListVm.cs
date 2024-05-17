using Domain.Models;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Assignments;

public class SolutionListVm
{
    public GetSolutionsQuery Filter { get; set; } = new();
    public List<Solution> Solutions { get; set; } = [];

    public List<SelectListItem> Subjects { get; set; } = [];

    public List<SelectListItem> HisStudents { get; set; } = [];
    public List<SelectListItem> HisTutors { get; set; } = [];
    public List<SelectListItem> Assignments { get; set; } = [];
}