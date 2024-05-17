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
            var dbTasks = await DatabaseContext.Assignments
                .Include(x => x.Subject)
                .Include(x => x.Solutions)
                .Include(x => x.Files)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.Tutor.Id == r.UserId)
                .ToListAsync();

            var lesList = Mapper.Map<List<Assignment>>(dbTasks);
            return lesList;
        }

        public GetAssignmentsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}

public class GetAssignmentNamesQuery : IRequest<Dictionary<int, string>>
{
    public int? TutorId { get; set; }
    public int? StudentId { get; set; }

    public class GetAssignmentNamesQueryHandler : BaseMediatrHandler<GetAssignmentNamesQuery, Dictionary<int, string>>
    {
        public override async Task<Dictionary<int, string>> Handle(GetAssignmentNamesQuery r, CancellationToken token)
        {
            var q = DatabaseContext.Assignments
                .Include(x => x.Solutions)
                .Where(x =>
                        !r.TutorId.HasValue || r.TutorId == x.TutorId || //Пошук завдань як вчителя
                        !r.StudentId.HasValue || x.Solutions.Any(s => s.StudentId == r.StudentId) //Пошук як учня
                ).AsQueryable();
            return await q.ToDictionaryAsync(k => k.Id, v => v.Title);
        }

        public GetAssignmentNamesQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}