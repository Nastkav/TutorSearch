using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;

namespace Domain.Commands;

public class CheckFavoriteCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public int TutorId { get; set; }
    public bool Status { get; set; }

    public class CheckFavoriteCommandHandler : BaseMediatrHandler<CheckFavoriteCommand, bool>
    {
        public CheckFavoriteCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(CheckFavoriteCommand r, CancellationToken token)
        {
            var tutorSaved = DatabaseContext.FavoriteTutors.Where(x =>
                    x.UserId == r.UserId
                    && x.ProfileId == r.TutorId)
                .FirstOrDefault();
            if (r.Status && tutorSaved == null)
                DatabaseContext.FavoriteTutors.Add(new FavoriteTutorModel
                {
                    ProfileId = r.TutorId,
                    UserId = r.UserId
                });
            else if (!r.Status && tutorSaved != null)
                DatabaseContext.FavoriteTutors.Remove(tutorSaved);
            await DatabaseContext.SaveChangesAsync();
            return true;
        }
    }
}