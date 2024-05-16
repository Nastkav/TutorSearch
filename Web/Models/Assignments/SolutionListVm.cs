using Domain.Models;
using Domain.Queries;

namespace Web.Models.Assignments;

public class SolutionListVm
{
    public GetSolutionsQuery Filter { get; set; } = new();
    public List<Solution> Solutions { get; set; } = [];
}