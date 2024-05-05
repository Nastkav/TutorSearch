using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetTutorsQuery : IRequest<List<TutorProfileModel>>
{
    public decimal? HourRateFrom { get; set; }
    public decimal? HourRateTo { get; set; }
    public string CityId { get; set; } = "0";

    public string SubjectId { get; set; } = "0";
    public bool OnlineAccess { get; set; }
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }

    public class GetTutorsQueryHandler : BaseMediatrHandler<GetTutorsQuery, List<TutorProfileModel>>
    {
        public override async Task<List<TutorProfileModel>> Handle(GetTutorsQuery r, CancellationToken token)
        {
            var q = ApplicationDb.Users
                .Include(x => x.TutorProfile)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.TutorProfileEnabled);

            if (r.HourRateFrom != null)
                q = q.Where(x => x.TutorProfile.HourRate > r.HourRateFrom);
            if (r.HourRateTo != null)
                q = q.Where(x => x.TutorProfile.HourRate < r.HourRateTo);
            if (r.CityId != "0")
                q = q.Where(x => x.CityId == int.Parse(r.CityId));
            if (r.SubjectId != "0")
                q = q.Where(x => x.TutorProfile.Subjects.Any(x => x.Id == int.Parse(r.SubjectId)));
            if (r.OnlineAccess)
                q = q.Where(x => x.TutorProfile.OnlineAccess);
            if (r.TutorHomeAccess)
                q = q.Where(x => x.TutorProfile.TutorHomeAccess);
            if (r.OnlineAccess)
                q = q.Where(x => x.TutorProfile.StudentHomeAccess);

            return q.Select(x => x.TutorProfile).ToList();
        }

        public GetTutorsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}