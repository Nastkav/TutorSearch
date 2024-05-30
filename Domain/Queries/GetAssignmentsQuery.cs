using System.ComponentModel;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetAssignmentsQuery : IRequest<List<Assignment>>
{
    public int UserId { get; set; }
    [DisplayName("Предмет")] public List<int> SubjectId { get; set; } = [];
    [DisplayName("Учень")] public List<int> StudentId { get; set; } = [];


    public class GetAssignmentsQueryHandler : BaseMediatrHandler<GetAssignmentsQuery, List<Assignment>>
    {
        public override async Task<List<Assignment>> Handle(GetAssignmentsQuery r, CancellationToken token)
        {
            //Запит
            var q = DatabaseContext.Assignments.AsNoTracking()
                .Include(x => x.Subject)
                .Include(x => x.Solutions)
                .Include(x => x.Files)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.TutorId == r.UserId)
                .AsQueryable();

            //Filters
            if (r.SubjectId.Count > 0)
                q = q.Where(x => r.SubjectId.Contains(x.SubjectId));
            if (r.StudentId.Count > 0)
                q = q.Where(x => x.Solutions.Any(s => r.StudentId.Contains(s.StudentId)));

            //Execute
            var dbAssignments = await q.ToListAsync();
            var assignments = Mapper.Map<List<Assignment>>(dbAssignments);
            return assignments;
        }

        public GetAssignmentsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}