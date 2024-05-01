using System.Diagnostics;
using AutoMapper;
using Domain.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Infra.Ports;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetTutorProfileQuery : IRequest<TutorDto>
{
    public int ProfileId { get; set; }

    public class GetTutorProfileQueryHandler : BaseMediatrHandler<GetTutorProfileQuery, TutorDto>
    {
        public override async Task<TutorDto> Handle(GetTutorProfileQuery r, CancellationToken token)
        {
            var dbProfile = ApplicationDb.TutorProfiles
                .Include(x => x.About)
                .Include(x => x.City)
                .Include(x => x.Subjects)
                .FirstOrDefault(x => x.Id == r.ProfileId);
            if (dbProfile == null)
            {
                if (!ApplicationDb.Users.Any(x => x.Id == r.ProfileId))
                    throw new Exception("Користувача не знайдено");
                dbProfile = new TutorProfileModel()
                {
                    CreatedBy = r.ProfileId,
                    Id = r.ProfileId
                };
                await ApplicationDb.AddAsync(dbProfile);
                await ApplicationDb.SaveChangesAsync();
            }


            var tutorDetails = Mapper.Map<TutorDto>(dbProfile);
            tutorDetails.City = dbProfile.City != null ? dbProfile.City.Name : "";
            tutorDetails.CityId = dbProfile.City != null ? dbProfile.City.Id : 0;
            tutorDetails.About = dbProfile.About.Content;
            tutorDetails.Enabled = dbProfile.Enabled;
            //TODO: Calc RatingValue
            //TODO: Calc ReviewCount

            foreach (var subject in dbProfile.Subjects)
                tutorDetails.Subjects[subject.Id] = subject.Name;

            return tutorDetails;
        }

        public GetTutorProfileQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}