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
            var dbTask = await DatabaseContext.Assignments.AsNoTracking()
                .Include(x => x.Subject)
                .Include(x => x.Solutions)
                .Include(x => x.Files)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.TutorId == r.UserId || x.Solutions.Any(y => y.StudentId == r.UserId))
                .FirstOrDefaultAsync(x => x.Id == r.AssignmentId);

            if (dbTask == null)
                throw new AssignmentException("Задачу не знайдено");

            var assignment = Mapper.Map<Assignment>(dbTask);
            return assignment;
        }

        public GetOneAssignmentQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}