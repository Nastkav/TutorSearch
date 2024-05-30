using Domain.Models;
using Domain.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Assignments;

public class AssignmentListVm
{
    public GetAssignmentsQuery Filter { get; set; } = new();
    public List<Assignment> Assignments { get; set; } = [];

    public List<SelectListItem> Subjects { get; set; } = [];
    public List<SelectListItem> HisStudents { get; set; } = [];
}