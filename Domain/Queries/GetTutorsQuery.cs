using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public decimal? HourRateFrom { get; set; }
    public decimal? HourRateTo { get; set; }
    public string CityId { get; set; } = "0";

    public string SubjectId { get; set; } = "0";
    public bool OnlineAccess { get; set; }
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }

    public string SearchText { get; set; } = string.Empty;

    public class GetTutorsQueryHandler : BaseMediatrHandler<GetTutorsQuery, List<int>>
    {
        public override async Task<List<int>> Handle(GetTutorsQuery r, CancellationToken token)
        {
            var q = ApplicationDb.Users
                .Include(x => x.Reviews)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.ProfileEnabled);

            if (r.HourRateFrom != null)
                q = q.Where(x => x.Tutor.HourRate > r.HourRateFrom);
            if (r.HourRateTo != null)
                q = q.Where(x => x.Tutor.HourRate < r.HourRateTo);
            if (r.CityId != "0")
                q = q.Where(x => x.CityId == int.Parse(r.CityId));
            if (r.SubjectId != "0")
                q = q.Where(x => x.Tutor.Subjects.Any(x => x.Id == int.Parse(r.SubjectId)));
            if (r.OnlineAccess)
                q = q.Where(x => x.Tutor.OnlineAccess);
            if (r.TutorHomeAccess)
                q = q.Where(x => x.Tutor.TutorHomeAccess);
            if (r.OnlineAccess)
                q = q.Where(x => x.Tutor.StudentHomeAccess);

            return await q.OrderBy(x => x.Reviews.Count).Select(x => x.Id).ToListAsync();
        }

        public GetTutorsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}