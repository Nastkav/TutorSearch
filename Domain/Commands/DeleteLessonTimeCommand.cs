using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Port.Driving;

namespace Domain.Commands;

public class DeleteLessonTimeCommand : IRequest<bool>
{
    public TimeType Type { get; set; }
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }

    public class DeleteLessonTimeCommandHandler : BaseMediatrHandler<DeleteLessonTimeCommand, bool>
    {
        public override async Task<bool> Handle(DeleteLessonTimeCommand r, CancellationToken token)
        {
            if (r.Type == TimeType.Available)
            {
                var lesson = ApplicationDb.AvailableTimes.Where(x => x.Id == r.EventId).FirstOrDefault();
                if (lesson == null)
                    throw new ArgumentNullException("Помилка в номері події");
                ApplicationDb.Remove(lesson);
            }
            else
            {
                var lesson = ApplicationDb.Lessons.Where(x => x.Id == r.EventId).FirstOrDefault();

                if (lesson == null)
                    throw new ArgumentNullException("Помилка в номері події");
                ApplicationDb.Remove(lesson);
            }

            await ApplicationDb.SaveChangesAsync();
            return true;
        }

        public DeleteLessonTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}