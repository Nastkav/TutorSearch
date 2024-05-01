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
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid? CourseId { get; set; }


    public class CreateLessonTimeCommandHandler : BaseMediatrHandler<CreateLessonTimeCommand, int>
    {
        public CreateLessonTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateLessonTimeCommand request, CancellationToken cancellationToken)
        {
            //On day event
            if (request.StartTime.Date != request.EndTime.Date)
                throw new Exception("Подія має бути протягом дня.");
            var weekday = (int)request.StartTime.DayOfWeek;
            var start = TimeOnly.FromDateTime(request.StartTime);
            var end = TimeOnly.FromDateTime(request.EndTime);


            switch (request.Type)
            {
                case TimeType.Available: //Встановлення робочого часу репетитора у кабінеті
                    var dbTimes = ApplicationDb.AvailableTimes
                        .Where(x => x.ProfileId == request.CreatedBy && x.DayOfWeek == weekday)
                        .OrderBy(x => x.StartTime)
                        .ToList();

                    //Перевірка перетинання часу
                    foreach (var timeRange in dbTimes)
                        if (timeRange.EndTime > start && timeRange.StartTime < end)
                            throw new Exception("Додавання неможливе, час перетинається");
                    //TODO: timeRange проверить проверку пересечения дат


                    var availableTime = new AvailableTime()
                    {
                        DayOfWeek = weekday,
                        StartTime = TimeOnly.FromDateTime(request.StartTime),
                        EndTime = TimeOnly.FromDateTime(request.EndTime),
                        ProfileId = request.CreatedBy,
                        CreatedBy = request.CreatedBy
                    };
                    ApplicationDb.AvailableTimes.Add(availableTime);
                    await ApplicationDb.SaveChangesAsync();
                    return availableTime.Id;
                case TimeType.Busy: //Додавання нового заняття
                    if (request.CourseId == null)
                        throw new Exception("Неправильний ідентифікатор курсу");
                    var students = ApplicationDb.Courses
                        .Where(x => x.Id == request.CourseId)
                        .Select(x => x.Students)
                        .FirstOrDefault();
                    if (students == null || students.Count == 0)
                        throw new Exception("Помилка в ідентифікаторі курсу або відсутні учні");

                    //Перевірка що входить до одного з доступних діапазонів
                    var availableRange = ApplicationDb.AvailableTimes
                        .Where(x => x.StartTime <= start && end <= x.EndTime && x.DayOfWeek == weekday)
                        .FirstOrDefault();
                    if (availableRange == null)
                        throw new Exception("Вибрано поза робочий час");

                    //Перевірка перетинання часу
                    //https://scicomp.stackexchange.com/questions/26258/the-easiest-way-to-find-intersection-of-two-intervals
                    var lessonOnRange = ApplicationDb.Lessons
                        .Where(x => x.End > request.StartTime || request.EndTime > x.Start).ToList();
                    if (lessonOnRange.Count > 0)
                        throw new Exception("Додавання неможливе, час перетинається");

                    //Додання
                    var newLesson = new LessonModel()
                    {
                        TutorProfileId = request.CreatedBy,
                        CourseId = request.CourseId.Value,
                        Students = students
                    };

                    ApplicationDb.Lessons.Add(newLesson);
                    await ApplicationDb.SaveChangesAsync();
                    return newLesson.Id;
                default:
                    throw new ArgumentOutOfRangeException("Неможливо додати подію типу: " + request.Type.ToString());
            }
        }
    }
}