using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Commands;

public class DeleteLessonTimeCommand : IRequest<bool>
{
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }

    public class DeleteLessonTimeCommandHandler : BaseMediatrHandler<DeleteLessonTimeCommand, bool>
    {
        public override async Task<bool> Handle(DeleteLessonTimeCommand request,
            CancellationToken cancellationToken)
        {
            var lesson = ApplicationDb.Lessons.Where(x => x.Id == request.EventId).First();
            ApplicationDb.Remove(lesson);
            await ApplicationDb.SaveChangesAsync();
            return true;
        }

        public DeleteLessonTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}