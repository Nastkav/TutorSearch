using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetAllSubjectsQuery : IRequest<Dictionary<int, string>>
{
    public int? TutorId { get; set; }

    public class GetAllSubjectsQueryHandler : BaseMediatrHandler<GetAllSubjectsQuery, Dictionary<int, string>>
    {
        public GetAllSubjectsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetAllSubjectsQuery r, CancellationToken token)
        {
            List<SubjectModel> subjects;
            if (r.TutorId.HasValue)
                subjects = await DatabaseContext.Tutors
                    .Include(x => x.Subjects)
                    .AsNoTracking()
                    .Where(x => x.Id == r.TutorId.Value)
                    .SelectMany(x => x.Subjects)
                    .ToListAsync();
            else
                subjects = await DatabaseContext.Subjects.AsNoTracking().ToListAsync();

            return subjects.ToDictionary(k => k.Id, v => v.Name);
        }
    }
}