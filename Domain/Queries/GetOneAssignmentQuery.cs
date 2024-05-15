using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetOneAssignmentQuery : IRequest<Assignment?>
{
    public int AssignmentId { get; set; }
    public int UserId { get; set; }

    public class GetOneAssignmentQueryHandler : BaseMediatrHandler<GetOneAssignmentQuery, Assignment?>
    {
        public override async Task<Assignment?> Handle(GetOneAssignmentQuery r, CancellationToken token)
        {
            //Запит
            var dbTask = await ApplicationDb.Assignments
                .Include(x => x.Subject)
                .Include(x => x.Solutions)
                .Include(x => x.Files)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.TutorId == r.UserId || x.Solutions.Any(y => y.StudentId == r.UserId))
                .FirstOrDefaultAsync(x => x.Id == r.AssignmentId);

            if (dbTask == null)
                throw new Exception("Задачу не знайдено");

            var task = Mapper.Map<Assignment>(dbTask);
            task.StudentSolutions = dbTask.Solutions.ToDictionary(k => k.StudentId, v => v.Id);
            task.AttachmentFile = null; //TODO: dbTasks.Files.ToDictionary(k => k.Id, v => v.Name);

            return task;
        }

        public GetOneAssignmentQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}