using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetUserProfileQuery : IRequest<User>
{
    public int ProfileId { get; set; }

    public class GetUserProfileQueryHandler : BaseMediatrHandler<GetUserProfileQuery, User>
    {
        public override async Task<User> Handle(GetUserProfileQuery r, CancellationToken token)
        {
            var dbProfile = await ApplicationDb.Users.FirstOrDefaultAsync(x => x.Id == r.ProfileId);

            //Перевірка на існування  користувача
            if (dbProfile == null)
                throw new Exception("Користувача не знайдено");

            var profile = Mapper.Map<User>(dbProfile);
            return profile;
        }

        public GetUserProfileQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}