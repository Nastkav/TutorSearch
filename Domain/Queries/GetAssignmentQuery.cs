using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetAssignmentQuery : IRequest<List<Assignment>>
{
    public int UserId { get; set; }

    public class GetTasksQueryHandler : BaseMediatrHandler<GetAssignmentQuery, List<Assignment>>
    {
        public override async Task<List<Assignment>> Handle(GetAssignmentQuery r, CancellationToken token)
        {
            //Запит
            var dbTasks = await ApplicationDb.Assignments
                .Include(x => x.Subject)
                .Include(x => x.Solutions)
                .Include(x => x.Files)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.Tutor.Id == r.UserId)
                .ToListAsync();

            var lesList = Mapper.Map<List<Assignment>>(dbTasks);
            for (var i = 0; i < lesList.Count; i++)
            {
                lesList[i].StudentSolutions = dbTasks[i].Solutions.ToDictionary(k => k.StudentId, v => v.Id);
                lesList[i].AttachmentFile = null; //TODO: dbTasks[i].Files.ToDictionary(k => k.Id, v => v.Name);
            }

            return lesList;
        }

        public GetTasksQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}