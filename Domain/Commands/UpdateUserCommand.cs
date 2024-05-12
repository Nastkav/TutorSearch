using System.Web;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class UpdateUserCommand : IRequest<bool>
{
    public required User Profile { get; set; }

    public class UpdateUserCommandHandler : BaseMediatrHandler<UpdateUserCommand, bool>
    {
        public UpdateUserCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(UpdateUserCommand r, CancellationToken token)
        {
            var dbUser = await ApplicationDb.Users.FirstOrDefaultAsync(x => x.Id == r.Profile.Id);
            //Check exist
            if (dbUser == null || dbUser.Id == 0 || !ApplicationDb.Users.Any(x => x.Id == dbUser.Id))
                throw new Exception("Ідентіфікатор користувача не знайдено.");
            //TODO: NewAvatarFileAvatar Load new Avatar
            Mapper.Map(r.Profile, dbUser);
            ApplicationDb.Users.Update(dbUser);
            await ApplicationDb.SaveChangesAsync(token);
            return true;
        }
    }
}