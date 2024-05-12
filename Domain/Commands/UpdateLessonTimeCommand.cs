using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;

namespace Domain.Commands;

public class UpdateSessionCommand : IRequest<int>
{
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string? Comment { get; set; }
    public string? Subject { get; set; }
    public int? SubjectId { get; set; }

    public class UpdateSessionCommandHandler : BaseMediatrHandler<UpdateSessionCommand, int>
    {
        public override async Task<int> Handle(UpdateSessionCommand r, CancellationToken token)
        {
            //On day event
            if (r.From.Date != r.To.Date)
                throw new Exception("Подія має бути протягом дня.");
            var weekday = (int)r.From.DayOfWeek;
            var start = TimeOnly.FromDateTime(r.From);
            var end = TimeOnly.FromDateTime(r.To);

            var dbLesson = ApplicationDb.Lessons.FirstOrDefault(x => x.Id == r.EventId);
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
                .Where(x => x.To > r.From || r.To > x.From).ToList();
            if (lessonOnRange.Count > 0)
                throw new Exception("Оновлення неможливе, час перетинається");

            //Time params
            dbLesson.From = r.From;
            dbLesson.To = r.To;
            //Subject
            if (r.SubjectId != null) dbLesson.SubjectId = r.SubjectId.Value;
            if (r.Subject != null)
            {
                var dbSubject = ApplicationDb.Subjects.FirstOrDefault(x => x.Name == r.Subject);
                if (dbSubject == null)
                    throw new Exception("Предмет не знайдено");
                dbLesson.Subject = dbSubject;
            }

            //Comment
            if (r.Comment != null) dbLesson.Comment = r.Comment;

            ApplicationDb.Lessons.Update(dbLesson);
            await ApplicationDb.SaveChangesAsync();
            return dbLesson.Id;
        }

        public UpdateSessionCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}