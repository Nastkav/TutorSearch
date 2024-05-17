using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetAllCitiesQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllCitiesQueryHandler : BaseMediatrHandler<GetAllCitiesQuery, Dictionary<int, string>>
    {
        public GetAllCitiesQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }


        public override async Task<Dictionary<int, string>> Handle(GetAllCitiesQuery r, CancellationToken token) =>
            await DatabaseContext.Cities.ToDictionaryAsync(k => k.Id, v => v.FullName());
    }
}

public class UserIsTutorQuery : IRequest<bool>
{
    public int UserId { get; set; }

    public class UserIsTutorQueryHandler : BaseMediatrHandler<UserIsTutorQuery, bool>
    {
        public UserIsTutorQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }


        public override async Task<bool> Handle(UserIsTutorQuery r, CancellationToken token) =>
            (await DatabaseContext.Users.FirstAsync(x => x.Id == r.UserId)).ProfileEnabled;
    }
}