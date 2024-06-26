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
            var dbProfile = await DatabaseContext.Users.AsNoTracking()
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == r.ProfileId);

            //Перевірка на існування  користувача
            if (dbProfile == null)
                throw new UserNotFoundException("Користувача не знайдено");

            var profile = Mapper.Map<User>(dbProfile);
            return profile;
        }

        public GetUserProfileQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}