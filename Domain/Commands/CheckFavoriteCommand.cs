using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AutoMapper.Internal;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class CheckFavoriteCommand : IRequest<bool>
{
    [Range(1, int.MaxValue)] public int UserId { get; set; }
    [Range(1, int.MaxValue)] public int TutorId { get; set; }
    public bool Status { get; set; }

    public class CheckFavoriteCommandHandler : BaseMediatrHandler<CheckFavoriteCommand, bool>
    {
        public CheckFavoriteCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }


        public override async Task<bool> Handle(CheckFavoriteCommand r, CancellationToken token)
        {
            var dbUser = await DatabaseContext.Users
                .Include(x => x.FavoriteTutors)
                .FirstOrDefaultAsync(x => x.Id == r.UserId);
            if (dbUser == null)
                throw new UserNotFoundException("Неправильний ідентифікатор користувача");

            var dbTutor = await DatabaseContext.Tutors.FirstOrDefaultAsync(x => x.Id == r.TutorId);
            if (dbTutor == null)
                throw new UserNotFoundException("Неправильний ідентифікатор вчителя");

            var dbFav = dbUser.FavoriteTutors.FirstOrDefault(x => x.Id == r.TutorId);
            if (dbFav == null && r.Status)
                dbUser.FavoriteTutors.Add(dbTutor);
            else if (dbFav != null && !r.Status)
                dbUser.FavoriteTutors.Remove(dbFav);


            await DatabaseContext.SaveChangesAsync();
            return r.Status;
        }
    }
}