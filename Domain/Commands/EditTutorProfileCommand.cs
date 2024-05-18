using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Domain.Helpers;
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
        public EditTutorProfileCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(UpdateTutorCommand r, CancellationToken token)
        {
            //Check exist
            if (!DatabaseContext.Users.Any(x => x.Id == r.Profile.Id && x.ProfileEnabled))
                throw new Exception("Ідентіфікатор вчителя не знайдено.");

            //Select from database
            var dbProfile = await DatabaseContext.Tutors
                                .Include(x => x.About)
                                .Include(x => x.Subjects)
                                .FirstOrDefaultAsync(x => x.Id == r.Profile.Id)
                            ?? new TutorModel();
            var newObject = dbProfile.Id == 0;

            //Mapping
            Mapper.Map(r.Profile, dbProfile);

            //Subjects
            dbProfile.Subjects = await DatabaseContext.Subjects
                .Where(x => r.Profile.SubjectIds.Contains(x.Id))
                .ToListAsync();

            if (newObject)
                DatabaseContext.Add(dbProfile);
            else
                DatabaseContext.Update(dbProfile);

            await DatabaseContext.SaveChangesAsync(token);
            return true;
        }
    }
}