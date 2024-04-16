using Domain.DrivingPort.Models;
using Domain.Port.Driving;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class GetLessonDetailQuery : IRequest<LessonDetailsDto>
{
    public int UserId { get; set; }
    public int LessonId { get; set; }

    public class GetLessonDetailQueryHandler : BaseMediatrHandler<GetLessonDetailQuery, LessonDetailsDto>
    {
        public GetLessonDetailQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<LessonDetailsDto> Handle(GetLessonDetailQuery request,
            CancellationToken cancellationToken)
        {
            //TODO: GetLessonDetailQueryHandler
            List<LessonDto> events = new();
            var dtMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            return new LessonDetailsDto()
            {
                Type = TimeType.Busy,
                Title = "Maths -- Petrov",
                StartTime = dtMonday.AddHours(10),
                EndTime = dtMonday.AddHours(12),
                Subject = "Maths",
                Comment = "https://link.to/AAAAAAAAAAAAAAA",
                CourseId = Guid.NewGuid(),
                Course = "German Pre-intermediate"
            };
        }
    }
}