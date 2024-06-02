using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetTutorsQuery : IRequest<List<int>>
{
    public int? HourRateFrom { get; set; }
    public int? HourRateTo { get; set; }
    public string CityId { get; set; } = "0";

    public string SubjectId { get; set; } = "0";
    public bool OnlineAccess { get; set; }
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }

    public string? SearchText { get; set; } = string.Empty;

    public int IdentityId { get; set; }
    public bool OnlyFavorites { get; set; }

    public class GetTutorsQueryHandler : BaseMediatrHandler<GetTutorsQuery, List<int>>
    {
        public override async Task<List<int>> Handle(GetTutorsQuery r, CancellationToken token)
        {
            var q = DatabaseContext.Users.AsNoTracking()
                .Include(x => x.Tutor)
                .ThenInclude(x => x.Subjects)
                .Include(x => x.Tutor.InFavorite)
                .Where(x => x.ProfileEnabled);

            if (r.HourRateFrom != null)
                q = q.Where(x => x.Tutor.HourRate >= r.HourRateFrom);
            if (r.HourRateTo != null)
                q = q.Where(x => x.Tutor.HourRate <= r.HourRateTo);
            if (r.CityId != "0")
                q = q.Where(x => x.CityId == int.Parse(r.CityId));
            if (r.SubjectId != "0")
                q = q.Where(x => x.Tutor.Subjects.Any(s => s.Id == int.Parse(r.SubjectId)));
            if (r.OnlineAccess)
                q = q.Where(x => x.Tutor.OnlineAccess);
            if (r.TutorHomeAccess)
                q = q.Where(x => x.Tutor.TutorHomeAccess);
            if (r.StudentHomeAccess)
                q = q.Where(x => x.Tutor.StudentHomeAccess);
            if (r.OnlyFavorites)
                q = q.Where(x => x.Tutor.InFavorite.Any(f => f.Id == r.IdentityId));

            List<int> tutorsIds;
            if (r.SearchText != null && r.SearchText != string.Empty)
            {
                var searchText = r.SearchText.ToUpperInvariant().Trim().Split(' ');
                foreach (var partSearch in searchText)
                    q = q.Where(x => x.NormalizeName.Contains(partSearch));
            }

            tutorsIds = await q.OrderBy(x => x.Reviews.Count).Select(x => x.Id).ToListAsync();
            return tutorsIds;
        }

        public GetTutorsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}