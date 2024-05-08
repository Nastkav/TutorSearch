using AutoMapper;
using Domain.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetEventQuery : IRequest<List<LessonEvent>>
{
    public List<TimeType> TimeTypes { get; set; } = [];

    public int UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public class GetEventQueryHandler : BaseMediatrHandler<GetEventQuery, List<LessonEvent>>
    {
        public override async Task<List<LessonEvent>> Handle(GetEventQuery r, CancellationToken token)
        {
            if (!r.From.HasValue) r.From = DateTime.MinValue;
            if (!r.To.HasValue) r.To = DateTime.MaxValue;

            List<LessonEvent> events = new();
            var dbLessons = ApplicationDb.Lessons
                .Where(x => x.Students.Any(y => y.Id == r.UserId) || x.TutorId == r.UserId)
                .Where(x => r.From < x.From && x.To < r.To)
                .ToList();

            var dbAvailableTime = ApplicationDb.AvailableTimes
                .Where(x => x.ProfileId == r.UserId)
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToList();

            //Calc Busy Time
            foreach (var lesson in dbLessons)
                events.Add(new LessonEvent
                {
                    Id = lesson.Id,
                    Type = TimeType.Busy,
                    Title = lesson.Title,
                    From = lesson.From,
                    To = lesson.To
                });

            //Available Time
            var prevSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            var availableTimes = new List<LessonEvent>();
            for (var i = 0; i <= 4; i++) //Розрахунок наступних тижнів 4
            {
                var offsetWeek = i * 7;
                foreach (var dbRange in dbAvailableTime)
                    availableTimes.Add(new LessonEvent
                    {
                        Id = dbRange.Id,
                        Type = TimeType.Available,
                        From = prevSunday.AddDays(offsetWeek + dbRange.DayOfWeek).AddTicks(dbRange.StartTime.Ticks),
                        To = prevSunday.AddDays(offsetWeek + dbRange.DayOfWeek).AddTicks(dbRange.EndTime.Ticks)
                    });
            }

            events.AddRange(availableTimes.Where(x => r.From < x.From && x.To < r.To));

            events = events.OrderBy(x => x.From).ToList();
            //Unavailable Time
            var unavailableTimes = new List<LessonEvent>();
            var prevEventEnd = DateTime.MinValue;
            for (var i = 0; i < availableTimes.Count; i++)
            {
                if (i == 0)
                    prevEventEnd = availableTimes[i].From.Date; // Фиксування почутку дня
                else if (availableTimes[i - 1].From.Day != availableTimes[i].From.Day)
                    prevEventEnd = availableTimes[i - 1].To; // Взяти останній час з минулого дня

                unavailableTimes.Add(new LessonEvent
                {
                    Type = TimeType.Unavailable,
                    From = prevEventEnd,
                    To = availableTimes[i].From
                });
                prevEventEnd = availableTimes[i].To;
            }

            events.AddRange(unavailableTimes);

            //Type Filter
            if (r.TimeTypes.Count > 0)
                events = events.Where(x => r.TimeTypes.Contains(x.Type)).ToList();

            return events.ToList();
        }

        public GetEventQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}