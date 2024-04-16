using Domain.DrivingPort.Models;
using Web.Models.Shared;

namespace Web.Models.Lesson;

public class LessonViewModel
{
    public List<CheckboxViewModel> Subjects { get; set; } = [];
    public Dictionary<int, string> Cities { get; set; } = [];
    public LessonDetailsDto? LessonDetails { get; set; }
}