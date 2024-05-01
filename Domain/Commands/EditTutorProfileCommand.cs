using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class EditTutorProfileCommand : IRequest<bool>
{
    public int RequestBy { get; set; }
    public TutorEditDto TutorEdit { get; set; } = null!;


    public class EditTutorProfileCommandHandler : BaseMediatrHandler<EditTutorProfileCommand, bool>
    {
        public EditTutorProfileCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(EditTutorProfileCommand request, CancellationToken cancellationToken)
        {
            var u = ApplicationDb.Users
                .Include(x => x.TutorProfile.About)
                .FirstOrDefault(x => x.Id == request.RequestBy);
            if (u == null)
                throw new Exception("Undefined User");
            if (u.TutorProfile == null)
                u.TutorProfile = new TutorProfileModel();
            //Basic Mapping
            Mapper.Map(request.TutorEdit, u.TutorProfile);
            if (u.TutorProfile.CityId == 0)
                u.TutorProfile.CityId = null;
            //About
            u.TutorProfile.About.Content = request.TutorEdit.About ?? "";
            //Subjects
            u.TutorProfile.Subjects = await ApplicationDb.Subjects
                .Where(x => request.TutorEdit.Subjects.Keys.Contains(x.Id)).ToListAsync();
            u.TutorProfile.Address = ""; //Fix empty mapping string 
            //CreateOrUpdate
            if (u.TutorProfile.CreatedBy == 0)
                u.TutorProfile.CreatedBy = request.RequestBy;
            else
                u.TutorProfile.UpdatedBy = request.RequestBy;

            u.TutorProfile.Enabled = true;
            ApplicationDb.TutorProfiles.Update(u.TutorProfile);
            await ApplicationDb.SaveChangesAsync();
            return true;
        }
    }
}