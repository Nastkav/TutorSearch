using System.ComponentModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Assignments;

public class AssignmentVm
{
    public int? UserId { get; set; }
    public Assignment Assignment { get; set; } = new();
    public List<SelectListItem> Subjects { get; set; } = [];

    public List<SelectListItem> HisStudents { get; set; } = [];
}