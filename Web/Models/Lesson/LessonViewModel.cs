using Domain.Models;
using Domain.Queries;

namespace Web.Models.LessonRequest;

public class LessonRequestViewModel
{
    public List<LessonRequestDto> MyRequests { get; set; } = null!;
    public List<LessonRequestDto> RequestsForMe { get; set; } = null!;
}