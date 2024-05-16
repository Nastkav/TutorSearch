using Domain.Models;
using Domain.Queries;

namespace Web.Models.Assignments;

public class AssignmentListVm
{
    public GetAssignmentsQuery Filter { get; set; } = new();
    public List<Assignment> Assignments { get; set; } = [];
}