using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class UserIsTutorQuery : IRequest<bool>
{
    public int UserId { get; set; }

    public class UserIsTutorQueryHandler : BaseMediatrHandler<UserIsTutorQuery, bool>
    {
        public UserIsTutorQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }


        public override async Task<bool> Handle(UserIsTutorQuery r, CancellationToken token)
        {
            //Перевірка на існування  користувача
            var dbProfile = await DatabaseContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == r.UserId);
            if (dbProfile == null)
                throw new UserNotFoundException("Користувача не знайдено");
            return dbProfile.ProfileEnabled;
        }
    }
}