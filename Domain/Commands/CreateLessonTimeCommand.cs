using AutoMapper;
using Domain.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateLessonTimeCommand : IRequest<int>
{
    public int CreatedBy { get; set; }

    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
    public List<int> Students { get; set; }


    public class CreateLessonTimeCommandHandler : BaseMediatrHandler<CreateLessonTimeCommand, int>
    {
        public CreateLessonTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateLessonTimeCommand r, CancellationToken token)
        {
            //On day event
            if (r.From.Date != r.To.Date)
                throw new Exception("Подія має бути протягом дня.");
            var weekday = (int)r.From.DayOfWeek;
            var start = TimeOnly.FromDateTime(r.From.DateTime);
            var end = TimeOnly.FromDateTime(r.To.DateTime);


            switch (r.Type)
            {
                case TimeType.Available: //Встановлення робочого часу репетитора у кабінеті
                    var dbTimes = ApplicationDb.AvailableTimes
                        .Where(x => x.ProfileId == r.CreatedBy && x.DayOfWeek == weekday)
                        .OrderBy(x => x.StartTime)
                        .ToList();

                    //Перевірка перетинання часу
                    foreach (var timeRange in dbTimes)
                        if (timeRange.EndTime > start && timeRange.StartTime < end)
                            throw new Exception("Додавання неможливе, час перетинається");
                    //TODO: timeRange проверить проверку пересечения дат


                    var availableTime = new AvailableTimeModel()
                    {
                        DayOfWeek = weekday,
                        StartTime = TimeOnly.FromDateTime(r.From.DateTime),
                        EndTime = TimeOnly.FromDateTime(r.To.DateTime),
                        ProfileId = r.CreatedBy,
                        CreatedId = r.CreatedBy
                    };
                    ApplicationDb.AvailableTimes.Add(availableTime);
                    await ApplicationDb.SaveChangesAsync();
                    return availableTime.Id;
                case TimeType.Busy: //Додавання нового заняття
                    var dbStudents = ApplicationDb.Users.Where(x => r.Students.Contains(x.Id)).ToList();
                    if (dbStudents.Count == 0)
                    {
                        throw new Exception("Відсутні учні");
                    }
                    else if (dbStudents.Count != r.Students.Count)
                    {
                        var unknownIds = dbStudents.Select(x => x.Id).Where(x => r.Students.Contains(x)).ToList();
                        throw new Exception($"Не знайдені учні з номерами {string.Join(", ", unknownIds)}");
                    }


                    //Перевірка що входить до одного з доступних діапазонів
                    var availableRange = ApplicationDb.AvailableTimes
                        .Where(x => x.StartTime <= start && end <= x.EndTime && x.DayOfWeek == weekday)
                        .FirstOrDefault();
                    if (availableRange == null)
                        throw new Exception("Вибрано поза робочий час");

                    //Перевірка перетинання часу
                    //https://scicomp.stackexchange.com/questions/26258/the-easiest-way-to-find-intersection-of-two-intervals
                    var lessonOnRange = ApplicationDb.Lessons
                        .Where(x => x.To > r.From || r.To > x.From).ToList();
                    if (lessonOnRange.Count > 0)
                        throw new Exception("Додавання неможливе, час перетинається");

                    //Додання
                    var newLesson = new LessonModel()
                    {
                        TutorId = r.CreatedBy,
                        Students = dbStudents
                    };

                    ApplicationDb.Lessons.Add(newLesson);
                    await ApplicationDb.SaveChangesAsync();
                    return newLesson.Id;
                default:
                    throw new ArgumentOutOfRangeException("Неможливо додати подію типу: " + r.Type.ToString());
            }
        }
    }
}