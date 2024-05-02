using Domain.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetLessonQuery : IRequest<List<LessonDto>>
{
    public List<TimeType> TimeTypes { get; set; } = [];

    public int UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public class GetLessonQueryHandler : BaseMediatrHandler<GetLessonQuery, List<LessonDto>>
    {
        public override async Task<List<LessonDto>> Handle(GetLessonQuery r, CancellationToken token)
        {
            List<LessonDto> events = new();
            var dbLessons = ApplicationDb.Lessons
                .Include(x => x.Course)
                .Where(x => x.Students.Any(y => y.Id == r.UserId) || x.TutorProfileId == r.UserId)
                .ToList();

            var dbAvailableTime = ApplicationDb.AvailableTimes
                .Where(x => x.ProfileId == r.UserId)
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToList();

            //Calc Busy Time
            foreach (var lesson in dbLessons)
                events.Add(new LessonDto
                {
                    Id = lesson.Id,
                    Type = TimeType.Busy,
                    Title = lesson.Course.Title,
                    From = lesson.From,
                    To = lesson.To
                });

            //Available Time
            var prevSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);

            var availableTimes = new List<LessonDto>();
            for (var i = -2; i <= 2; i++) //+- 2 тижня
            {
                var offsetWeek = i * 7;
                foreach (var dbRange in dbAvailableTime)
                    availableTimes.Add(new LessonDto
                    {
                        Type = TimeType.Available,
                        From = prevSunday.AddDays(offsetWeek + dbRange.DayOfWeek).AddTicks(dbRange.StartTime.Ticks),
                        To = prevSunday.AddDays(offsetWeek + dbRange.DayOfWeek).AddTicks(dbRange.EndTime.Ticks)
                    });
            }

            events.AddRange(availableTimes);

            //Unavailable Time
            var unavailableTimes = new List<LessonDto>();
            var prevEventEnd = DateTime.MinValue;
            for (var i = 0; i < availableTimes.Count; i++)
            {
                if (i == 0 || availableTimes[i - 1].From.Day != availableTimes[i].From.Day)
                    prevEventEnd = availableTimes[i].From.Date; // Фиксування почутку дня

                unavailableTimes.Add(new LessonDto
                {
                    Type = TimeType.Unavailable,
                    From = prevEventEnd,
                    To = availableTimes[i].From
                });
                prevEventEnd = unavailableTimes[i].To;
            }

            events.AddRange(unavailableTimes);

            //Type Filter
            if (r.TimeTypes.Count > 0)
                events = events.Where(x => r.TimeTypes.Contains(x.Type)).ToList();

            return events;
        }

        public GetLessonQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}