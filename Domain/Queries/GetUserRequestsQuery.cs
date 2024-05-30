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
            var q = DatabaseContext.Requests.AsNoTracking()
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .Include(x => x.Created).AsQueryable();

            if (r.IsTutor)
                q = q.Where(x => x.TutorId == r.UserId);
            else
                q = q.Where(x => x.CreatedId == r.UserId);

            q = q.OrderBy(x => x.Status); // Перші ті, що не оброблені

            foreach (var dbRequest in await q.ToArrayAsync())
            {
                var req = Mapper.Map<LessonRequest>(dbRequest);
                req.TutorName = dbRequest.Tutor.User.FullName();
                req.Subject = dbRequest.Subject.Name;
                req.IsTutor = r.IsTutor;
                req.UserName = dbRequest.Created.FullName();
                listRequest.Add(req);
            }

            return listRequest;
        }

        public GetUserRequestsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}