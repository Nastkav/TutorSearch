using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class DeleteLessonCommand : IRequest<bool>
{
    public int LessonId { get; set; }
    public int TutorId { get; set; }

    public class DeleteLessonCommandHandler : BaseMediatrHandler<DeleteLessonCommand, bool>
    {
        public override async Task<bool> Handle(DeleteLessonCommand r, CancellationToken token)
        {
            var lesson = await ApplicationDb.Lessons
                .FirstOrDefaultAsync(x => x.TutorId == r.TutorId && x.Id == r.LessonId);
            if (lesson == null)
                return false;

            ApplicationDb.Remove(lesson);
            await ApplicationDb.SaveChangesAsync();
            return true;
        }

        public DeleteLessonCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}