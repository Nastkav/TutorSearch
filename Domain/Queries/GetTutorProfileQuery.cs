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

public class GetTutorProfileQuery : IRequest<Tutor>
{
    public int ProfileId { get; set; }

    public class GetTutorProfileQueryHandler : BaseMediatrHandler<GetTutorProfileQuery, Tutor>
    {
        public override async Task<Tutor> Handle(GetTutorProfileQuery r, CancellationToken token)
        {
            var dbProfile = ApplicationDb.TutorProfiles
                .Include(x => x.About)
                .Include(x => x.Subjects)
                .FirstOrDefault(x => x.Id == r.ProfileId);

            //Перевірка на існування  профілю
            if (dbProfile == null)
                if (ApplicationDb.Users.Any(x => x.Id == r.ProfileId))
                    return new Tutor(); //Новий профіль
                else
                    throw new Exception("Профіль вчителя не знайдено");

            var profile = Mapper.Map<Tutor>(dbProfile);
            //Додати активні тематики
            foreach (var subject in dbProfile.Subjects)
                profile.Subjects[subject.Id] = subject.Name;

            return profile;
        }

        public GetTutorProfileQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}