using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Port.Driving;

namespace Domain.Commands;

public class DeleteEventTimeCommand : IRequest<bool>
{
    public TimeType Type { get; set; }
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }

    public class DeleteEventTimeCommandHandler : BaseMediatrHandler<DeleteEventTimeCommand, bool>
    {
        public override async Task<bool> Handle(DeleteEventTimeCommand r, CancellationToken token)
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

        public DeleteEventTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}