using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;

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
                var lesson = DatabaseContext.AvailableTimes.Where(x => x.Id == r.EventId).FirstOrDefault();
                if (lesson == null)
                    throw new ArgumentNullException("Помилка в номері події");
                DatabaseContext.Remove(lesson);
            }
            else
            {
                var lesson = DatabaseContext.Lessons.Where(x => x.Id == r.EventId).FirstOrDefault();

                if (lesson == null)
                    throw new ArgumentNullException("Помилка в номері події");
                DatabaseContext.Remove(lesson);
            }

            await DatabaseContext.SaveChangesAsync();
            return true;
        }

        public DeleteSessionCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}