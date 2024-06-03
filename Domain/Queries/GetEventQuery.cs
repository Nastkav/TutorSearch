using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetEventQuery : IRequest<List<LessonSession>>
{
    public List<TimeType> TimeTypes { get; set; } = [];

    public int UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public class GetEventQueryHandler : BaseMediatrHandler<GetEventQuery, List<LessonSession>>
    {
        public override async Task<List<LessonSession>> Handle(GetEventQuery r, CancellationToken token)
        {
            if (!r.From.HasValue) r.From = DateTime.MinValue;
            if (!r.To.HasValue) r.To = DateTime.MaxValue;

            List<LessonSession> events = new();
            var dbLessons = await DatabaseContext.Lessons.AsNoTracking()
                .Where(x => x.Students.Any(y => y.Id == r.UserId) || x.TutorId == r.UserId)
                .Where(x => r.From <= x.To && x.From <= r.To)
                .ToListAsync();

            var dbAvailableTime = await DatabaseContext.AvailableTimes.AsNoTracking()
                .Where(x => x.ProfileId == r.UserId)
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.StartTime)
                .ToListAsync();

            //Calc Busy Time
            foreach (var lesson in dbLessons)
                events.Add(new LessonSession
                {
                    Id = lesson.Id,
                    Type = TimeType.Busy,
                    Title = lesson.Title,
                    From = lesson.From > r.From.Value ? lesson.From : r.From.Value,
                    To = lesson.To < r.To.Value ? lesson.To : r.To.Value
                });

            //Available Time
            var prevSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            var availableTimes = new List<LessonSession>();
            for (var i = -1; i <= 4; i++) //Розрахунок наступних тижнів 4
            {
                var offsetWeek = i * 7;
                foreach (var dbRange in dbAvailableTime)
                    availableTimes.Add(new LessonSession
                    {
                        Id = dbRange.Id,
                        Type = TimeType.Available,
                        From = prevSunday.AddDays(offsetWeek + dbRange.DayOfWeek).AddTicks(dbRange.StartTime.Ticks),
                        To = prevSunday.AddDays(offsetWeek + dbRange.DayOfWeek).AddTicks(dbRange.EndTime.Ticks)
                    });
            }

            availableTimes = availableTimes.Where(x => r.From <= x.From && x.To <= r.To).ToList();
            events.AddRange(availableTimes);

            events = events.OrderBy(x => x.From).ToList();

            //Unavailable Time
            var unavailableTimes = new List<LessonSession>();
            var prevEventEnd = r.From.Value; // Фиксування почутку дня

            //коригування на початок тижня
            if (availableTimes.Count > 0 && availableTimes[0].From != r.From)
                unavailableTimes.Add(new LessonSession()
                {
                    Type = TimeType.Unavailable,
                    From = r.From.Value,
                    To = availableTimes[0].From
                });


            for (var i = 0; i < availableTimes.Count; i++)
            {
                if (i != 0 && availableTimes[i - 1].From.Day != availableTimes[i].From.Day) //Якщо дата не та
                    prevEventEnd = availableTimes[i - 1].To; // Взяти останній час з минулого дня

                if ((availableTimes[i].From - prevEventEnd).TotalMinutes != 0) //Якщо події не йдуть поспіль
                    unavailableTimes.Add(new LessonSession
                    {
                        Type = TimeType.Unavailable,
                        From = prevEventEnd,
                        To = availableTimes[i].From
                    });

                prevEventEnd = availableTimes[i].To;
            }


            //коригування на кінець тижня
            if (availableTimes.Count == 0 || availableTimes[availableTimes.Count - 1].To != r.To)
                unavailableTimes.Add(new LessonSession()
                {
                    Type = TimeType.Unavailable,
                    From = prevEventEnd,
                    To = r.To.Value
                });

            events.AddRange(unavailableTimes);

            //Type Filter
            if (r.TimeTypes.Count > 0)
                events = events.Where(x => r.TimeTypes.Contains(x.Type)).ToList();

            return events.ToList();
        }

        public GetEventQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}