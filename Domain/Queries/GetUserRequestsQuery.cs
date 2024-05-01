using System.Runtime.InteropServices.JavaScript;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetUserRequestsQuery : IRequest<List<LessonRequestDto>>
{
    public int? UserId { get; set; }
    public bool OnlyActive { get; set; }

    public class GetUserRequestsQueryHandler : BaseMediatrHandler<GetUserRequestsQuery, List<LessonRequestDto>>
    {
        public override async Task<List<LessonRequestDto>> Handle(GetUserRequestsQuery r, CancellationToken token)
        {
            List<LessonRequestDto> listRequest = new();
            var dbRequests = await ApplicationDb.Requests
                .Include(x => x.Tutor.Owner)
                .Include(x => x.Subject)
                .Where(x => x.CreatedBy == r.UserId)
                .ToListAsync();

            foreach (var dbRequest in dbRequests)
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

                req.TutorName = dbRequest.Tutor.Owner.FullName();
                req.Subject = dbRequest.Subject.Name;
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