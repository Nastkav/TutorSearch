using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Queries;

public class GetAllSubjectsQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllSubjectsQueryHandler : BaseMediatrHandler<GetAllSubjectsQuery, Dictionary<int, string>>
    {
        public GetAllSubjectsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetAllSubjectsQuery request,
            CancellationToken cancellationToken) =>
            ApplicationDb.Subjects.ToDictionary(k => k.Id, v => v.Name);
    }
}