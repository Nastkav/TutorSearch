using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetAllSubjectsQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllSubjectsQueryHandler : BaseMediatrHandler<GetAllSubjectsQuery, Dictionary<int, string>>
    {
        public GetAllSubjectsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetAllSubjectsQuery r, CancellationToken token) =>
            await DatabaseContext.Subjects.ToDictionaryAsync(k => k.Id, v => v.Name);
    }
}