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
        public UpdateUserCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(UpdateUserCommand r, CancellationToken token)
        {
            var dbUser = await DatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == r.Profile.Id);
            //Check exist
            if (dbUser == null || dbUser.Id == 0 || !DatabaseContext.Users.Any(x => x.Id == dbUser.Id))
                throw new UserNotFoundException("Ідентіфікатор користувача не знайдено.");
            Mapper.Map(r.Profile, dbUser);
            DatabaseContext.Users.Update(dbUser);
            await DatabaseContext.SaveChangesAsync(token);
            return true;
        }
    }
}