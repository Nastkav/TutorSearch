using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Port.Driving;

namespace Domain.Commands;

public class DeleteSessionCommand : IRequest<bool>
{
    public TimeType Type { get; set; }
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }

    public class DeleteSessionCommandHandler : BaseMediatrHandler<DeleteSessionCommand, bool>
    {
        public override async Task<bool> Handle(DeleteSessionCommand r, CancellationToken token)
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

        public DeleteSessionCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}