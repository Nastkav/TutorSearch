using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Port.Driving;
using Infra.DatabaseAdapter.Models;

namespace Domain.Commands;

public class UpdateLessonTimeCommand : IRequest<int>
{
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Comment { get; set; }
    public string? Subject { get; set; }

    public class UpdateLessonTimeCommandHandler : BaseMediatrHandler<UpdateLessonTimeCommand, int>
    {
        public override async Task<int> Handle(UpdateLessonTimeCommand request, CancellationToken token)
        {
            //On day event
            if (request.StartTime.Date != request.EndTime.Date)
                throw new Exception("Подія має бути протягом дня.");
            var weekday = (int)request.StartTime.DayOfWeek;
            var start = TimeOnly.FromDateTime(request.StartTime);
            var end = TimeOnly.FromDateTime(request.EndTime);

            var dbLesson = ApplicationDb.Lessons.FirstOrDefault(x => x.Id == request.EventId);
            if (dbLesson == null)
                throw new Exception("Подію не знайдено");

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
                throw new Exception("Оновлення неможливе, час перетинається");

            //Time params
            dbLesson.Start = request.StartTime;
            dbLesson.End = request.EndTime;
            //Subject
            if (request.Subject != null)
            {
                var dbSubject = ApplicationDb.Subjects.FirstOrDefault(x => x.Name == request.Subject);
                if (dbSubject == null)
                    throw new Exception("Предмет не знайдено");
                dbLesson.Subject = dbSubject;
            }

            //Comment
            if (request.Comment != null) dbLesson.Comment = request.Comment;

            dbLesson.UpdatedBy = request.UpdatedBy;
            ApplicationDb.Lessons.Update(dbLesson);
            await ApplicationDb.SaveChangesAsync();
            return dbLesson.Id;
        }

        public UpdateLessonTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}