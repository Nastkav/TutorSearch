using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class CheckFavoriteQuery : IRequest<bool>
{
    public int TutorId { get; set; }
    public int UserId { get; set; }

    public class CheckFavoriteQueryHandler : BaseMediatrHandler<CheckFavoriteQuery, bool>
    {
        public override async Task<bool> Handle(CheckFavoriteQuery r, CancellationToken token)
        {
            var tutorSaved = await DatabaseContext.Users.AsNoTracking().AnyAsync(x =>
                x.Id == r.UserId &&
                x.FavoriteTutors.Any(t => t.Id == r.TutorId));
            return tutorSaved;
        }

        public CheckFavoriteQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}