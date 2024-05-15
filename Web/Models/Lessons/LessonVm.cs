using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Lessons;

public class LessonVm
{
    public int UserId { get; set; }
    public bool IsTutor { get; set; }
    public List<SelectListItem> Subjects { get; set; } = [];
    public Lesson Lesson { get; set; } = new();
    public List<SelectListItem> HisStudents { get; set; } = [];
}