using System.Runtime.InteropServices.JavaScript;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetUserRequestsQuery : IRequest<List<LessonRequest>>
{
    public int? UserId { get; set; }

    public bool IsTutor { get; set; }

    public class GetUserRequestsQueryHandler : BaseMediatrHandler<GetUserRequestsQuery, List<LessonRequest>>
    {
        public override async Task<List<LessonRequest>> Handle(GetUserRequestsQuery r, CancellationToken token)
        {
            List<LessonRequest> listRequest = new();
            var q = ApplicationDb.Requests
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .Include(x => x.Created).AsQueryable();

            if (r.IsTutor)
                q = q.Where(x => x.TutorId == r.UserId);
            else
                q = q.Where(x => x.CreatedId == r.UserId);

            foreach (var dbRequest in await q.ToArrayAsync())
            {
                var req = Mapper.Map<LessonRequest>(dbRequest);
                req.TutorName = dbRequest.Tutor.User.Name;
                req.Subject = dbRequest.Subject.Name;
                req.IsTutor = r.IsTutor;
                req.UserName = dbRequest.Created.Name;
                listRequest.Add(req);
            }

            return listRequest;
        }

        public GetUserRequestsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}