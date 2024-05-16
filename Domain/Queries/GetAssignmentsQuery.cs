using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetAssignmentsQuery : IRequest<List<Assignment>>
{
    public int UserId { get; set; }

    public class GetAssignmentsQueryHandler : BaseMediatrHandler<GetAssignmentsQuery, List<Assignment>>
    {
        public override async Task<List<Assignment>> Handle(GetAssignmentsQuery r, CancellationToken token)
        {
            //Запит
            var dbTasks = await ApplicationDb.Assignments
                .Include(x => x.Subject)
                .Include(x => x.Solutions)
                .Include(x => x.Files)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.Tutor.Id == r.UserId)
                .ToListAsync();

            var lesList = Mapper.Map<List<Assignment>>(dbTasks);
            return lesList;
        }

        public GetAssignmentsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}