using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateSessionCommand : IRequest<int>
{
    public int CreatedBy { get; set; }

    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }


    public class CreateSessionCommandHandler : BaseMediatrHandler<CreateSessionCommand, int>
    {
        public CreateSessionCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateSessionCommand r, CancellationToken token)
        {
            //On day event
            if (r.From.Date != r.To.Date)
                throw new TimeRangeException("Подія має бути протягом дня.");
            else if (r.From > r.To || r.From == r.To)
                throw new TimeRangeException("Обрано невірний час");

            var weekday = (int)r.From.DayOfWeek;
            var start = TimeOnly.FromDateTime(r.From.DateTime);
            var end = TimeOnly.FromDateTime(r.To.DateTime);

            switch (r.Type)
            {
                case TimeType.Available: //Встановлення робочого часу репетитора у кабінеті
                    var dbTimes = DatabaseContext.AvailableTimes
                        .Where(x => x.ProfileId == r.CreatedBy && x.DayOfWeek == weekday)
                        .OrderBy(x => x.StartTime)
                        .ToList();

                    //Перевірка перетинання часу
                    foreach (var timeRange in dbTimes)
                        if (timeRange.EndTime > start && timeRange.StartTime < end)
                            throw new TimeRangeException("Додавання неможливе, час перетинається");

                    var availableTime = new AvailableTimeModel()
                    {
                        DayOfWeek = weekday,
                        StartTime = TimeOnly.FromDateTime(r.From.DateTime),
                        EndTime = TimeOnly.FromDateTime(r.To.DateTime),
                        ProfileId = r.CreatedBy,
                        CreatedId = r.CreatedBy
                    };
                    DatabaseContext.AvailableTimes.Add(availableTime);
                    await DatabaseContext.SaveChangesAsync();
                    return availableTime.Id;
                default:
                    throw new ArgumentOutOfRangeException("Неможливо додати подію типу: " + r.Type.ToString());
            }
        }
    }
}