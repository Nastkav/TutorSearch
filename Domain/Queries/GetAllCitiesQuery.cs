using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetAllCitiesQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllCitiesQueryHandler : BaseMediatrHandler<GetAllCitiesQuery, Dictionary<int, string>>
    {
        public GetAllCitiesQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }


        public override async Task<Dictionary<int, string>> Handle(GetAllCitiesQuery r, CancellationToken token) =>
            ApplicationDb.Cities.ToDictionary(k => k.Id, v => v.FullName());
    }
}

public class UserIsTutorQuery : IRequest<bool>
{
    public int UserId { get; set; }

    public class UserIsTutorQueryHandler : BaseMediatrHandler<UserIsTutorQuery, bool>
    {
        public UserIsTutorQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }


        public override async Task<bool> Handle(UserIsTutorQuery r, CancellationToken token) =>
            (await ApplicationDb.Users.FirstAsync(x => x.Id == r.UserId)).ProfileEnabled;
    }
}