using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Shared;

namespace Web.Models.Lesson;

public class LessonViewModel
{
    public int UserId { get; set; }
    public bool IsTutor { get; set; }
    public List<SelectListItem> Subjects { get; set; } = [];
    public Domain.Models.Lesson Lesson { get; set; } = new();

    public List<SelectListItem> HisStudents { get; set; } = [];
}