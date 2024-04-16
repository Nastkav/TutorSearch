using Domain.DrivingPort.Models;
using Domain.Port.Driving;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class GetLessonQuery : IRequest<List<LessonDto>>
{
    public List<TimeType> TimeTypes { get; set; } = [];

    public int UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public class GetLessonQueryHandler : BaseMediatrHandler<GetLessonQuery, List<LessonDto>>
    {
        public GetLessonQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<List<LessonDto>> Handle(GetLessonQuery request,
            CancellationToken cancellationToken)
        {
            //TODO: GetLessonQueryHandler
            List<LessonDto> events = new();
            var dtMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            for (var i = 1; i <= 7; i++)
            {
                events.Add(new LessonDto
                {
                    Type = TimeType.Unavailable,
                    Title = "Title-1-" + i,
                    StartTime = dtMonday.AddDays(i - 1).AddHours(0),
                    EndTime = dtMonday.AddDays(i - 1).AddHours(7)
                });
                events.Add(new LessonDto
                {
                    Type = TimeType.Available,
                    Title = "Title-1-" + i,
                    StartTime = dtMonday.AddDays(i - 1).AddHours(7),
                    EndTime = dtMonday.AddDays(i - 1).AddHours(12)
                });
                events.Add(new LessonDto
                {
                    Type = TimeType.Busy,
                    Title = "Title-1-" + i,
                    StartTime = dtMonday.AddDays(i - 1).AddHours(12),
                    EndTime = dtMonday.AddDays(i - 1).AddHours(13)
                });
                events.Add(new LessonDto
                {
                    Type = TimeType.Available,
                    Title = "Title-2-" + i,
                    StartTime = dtMonday.AddDays(i - 1).AddHours(13),
                    EndTime = dtMonday.AddDays(i - 1).AddHours(20)
                });
                events.Add(new LessonDto
                {
                    Type = TimeType.Unavailable,
                    Title = "Title-1-" + i,
                    StartTime = dtMonday.AddDays(i - 1).AddHours(20),
                    EndTime = dtMonday.AddDays(i - 1).AddHours(24)
                });
            }

            //Type Filter
            if (request.TimeTypes.Count > 0)
                events = events.Where(x => request.TimeTypes.Contains(x.Type)).ToList();

            return events;
        }
    }
}