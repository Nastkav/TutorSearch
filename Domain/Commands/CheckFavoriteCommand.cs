using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Commands;

public class CheckFavoriteCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public int TutorId { get; set; }
    public bool Status { get; set; }

    public class CheckFavoriteCommandHandler : BaseMediatrHandler<CheckFavoriteCommand, bool>
    {
        public CheckFavoriteCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(CheckFavoriteCommand r, CancellationToken token)
        {
            var tutorSaved = ApplicationDb.FavoriteTutors.Where(x =>
                    x.UserId == r.UserId
                    && x.ProfileId == r.TutorId)
                .FirstOrDefault();
            if (r.Status && tutorSaved == null)
                ApplicationDb.FavoriteTutors.Add(new FavoriteTutorModel
                {
                    ProfileId = r.TutorId,
                    UserId = r.UserId
                });
            else if (!r.Status && tutorSaved != null)
                ApplicationDb.FavoriteTutors.Remove(tutorSaved);
            await ApplicationDb.SaveChangesAsync();
            return true;
        }
    }
}