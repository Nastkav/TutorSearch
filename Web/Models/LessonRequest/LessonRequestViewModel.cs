using Domain.Commands;
using Domain.Models;
using Infra.DatabaseAdapter.Helpers;

namespace Web.Models.LessonRequest;

public class LessonRequestVm
{
    public List<Domain.Models.LessonRequest> MyRequests { get; set; } = [];
    public List<Domain.Models.LessonRequest> RequestsForMe { get; set; } = [];

    public UpdateRequestCommand RequestUpdate { get; set; } = new();
}