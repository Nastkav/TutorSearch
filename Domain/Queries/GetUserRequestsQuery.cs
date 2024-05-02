using System.Runtime.InteropServices.JavaScript;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Entities;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetUserRequestsQuery : IRequest<List<LessonRequestDto>>
{
    public int? UserId { get; set; }

    public bool IsTutor { get; set; }

    public class GetUserRequestsQueryHandler : BaseMediatrHandler<GetUserRequestsQuery, List<LessonRequestDto>>
    {
        public override async Task<List<LessonRequestDto>> Handle(GetUserRequestsQuery r, CancellationToken token)
        {
            List<LessonRequestDto> listRequest = new();
            var q = ApplicationDb.Requests
                .Include(x => x.Tutor.Created)
                .Include(x => x.Subject)
                .Include(x => x.Created).AsQueryable();

            if (r.IsTutor)
                q = q.Where(x => x.TutorId == r.UserId);
            else
                q = q.Where(x => x.CreatedId == r.UserId);

            foreach (var dbRequest in await q.ToArrayAsync())
            {
                var req = Mapper.Map<LessonRequestDto>(dbRequest);
                switch (dbRequest.Status)
                {
                    case CourseRequestStatus.New:
                        req.Answer = null;
                        break;
                    case CourseRequestStatus.Approved:
                        req.Answer = true;
                        break;
                    case CourseRequestStatus.Rejected:
                        req.Answer = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                req.TutorName = dbRequest.Tutor.Created.FullName();
                req.Subject = dbRequest.Subject.Name;
                req.IsTutor = r.IsTutor;
                req.UserName = dbRequest.Created.FullName();
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