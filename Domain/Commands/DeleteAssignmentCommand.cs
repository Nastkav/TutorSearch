using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class DeleteAssignmentCommand : IRequest<bool>
{
    public int AssignmentId { get; set; }
    public int TutorId { get; set; }

    public class DeleteAssignmentCommandHandler : BaseMediatrHandler<DeleteAssignmentCommand, bool>
    {
        public override async Task<bool> Handle(DeleteAssignmentCommand r, CancellationToken token)
        {
            var assignment = await ApplicationDb.Assignments
                .FirstOrDefaultAsync(x => x.TutorId == r.TutorId && x.Id == r.AssignmentId);
            if (assignment == null)
                return false;

            ApplicationDb.Remove(assignment);
            await ApplicationDb.SaveChangesAsync();
            return true;
        }

        public DeleteAssignmentCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}