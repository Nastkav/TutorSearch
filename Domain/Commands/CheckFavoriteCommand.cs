using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Commands;

public class CheckFavoriteCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public int TutorProfileId { get; set; }
    public bool Status { get; set; }

    public class CheckFavoriteCommandHandler : BaseMediatrHandler<CheckFavoriteCommand, bool>
    {
        public CheckFavoriteCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(CheckFavoriteCommand request, CancellationToken cancellationToken)
        {
            var tutorSaved = ApplicationDb.FavoriteTutors.Where(x =>
                    x.UserId == request.UserId
                    && x.ProfileId == request.TutorProfileId)
                .FirstOrDefault();
            if (request.Status && tutorSaved == null)
                ApplicationDb.FavoriteTutors.Add(new FavoriteTutorModel
                {
                    ProfileId = request.TutorProfileId,
                    UserId = request.UserId
                });
            else if (!request.Status && tutorSaved != null)
                ApplicationDb.FavoriteTutors.Remove(tutorSaved);
            await ApplicationDb.SaveChangesAsync();
            return true;
        }
    }
}