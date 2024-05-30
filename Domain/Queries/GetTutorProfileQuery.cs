using System.Diagnostics;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
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
            var dbProfile = await DatabaseContext.Tutors.AsNoTracking()
                .Include(x => x.About)
                .Include(x => x.Subjects)
                .Include(x => x.Reviews)
                .Include(x => x.TeachingLessons)
                .FirstOrDefaultAsync(x => x.Id == r.ProfileId);

            //Перевірка на існування  профілю
            if (dbProfile == null)
                if (await DatabaseContext.Users.AsNoTracking().AnyAsync(x => x.Id == r.ProfileId))
                    return new Tutor(); //Новий профіль
                else
                    throw new UserNotFoundException("Профіль вчителя не знайдено");

            var profile = Mapper.Map<Tutor>(dbProfile);
            if (dbProfile.Reviews.Count > 0)
                profile.ReviewValue = (int)dbProfile.Reviews.Average(x => x.Rating);
            return profile;
        }

        public GetTutorProfileQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}