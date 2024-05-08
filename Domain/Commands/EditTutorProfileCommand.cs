using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class UpdateTutorCommand : IRequest<bool>
{
    public required Tutor Profile { get; set; }

    public class EditTutorProfileCommandHandler : BaseMediatrHandler<UpdateTutorCommand, bool>
    {
        public EditTutorProfileCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(UpdateTutorCommand r, CancellationToken token)
        {
            //Check exist
            if (!ApplicationDb.Users.Any(x => x.Id == r.Profile.Id && x.ProfileEnabled))
                throw new Exception("Ідентіфікатор вчителя не знайдено.");

            //Select from database
            var dbProfile = await ApplicationDb.Tutor
                                .Include(x => x.About)
                                .Include(x => x.Subjects)
                                .FirstOrDefaultAsync(x => x.Id == r.Profile.Id)
                            ?? new TutorModel();
            var newObject = dbProfile.Id == 0;

            //Mapping
            Mapper.Map(r.Profile, dbProfile);

            //Subjects
            dbProfile.Subjects = await ApplicationDb.Subjects
                .Where(x => r.Profile.SubjectIds.Contains(x.Id))
                .ToListAsync();

            if (newObject)
                ApplicationDb.Add(dbProfile);
            else
                ApplicationDb.Update(dbProfile);

            await ApplicationDb.SaveChangesAsync(token);
            return true;
        }
    }
}