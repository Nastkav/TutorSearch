using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetTutorStudentsQuery : IRequest<Dictionary<int, string>>
{
    public int TutorId { get; set; }

    public class GetTutorStudentsQueryHandler : BaseMediatrHandler<GetTutorStudentsQuery, Dictionary<int, string>>
    {
        public GetTutorStudentsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetTutorStudentsQuery r, CancellationToken token)
        {
            //Вибір усіх студентів, у яких був хоча б один урок з репетитором
            var students = await DatabaseContext.Requests.AsNoTracking()
                .Include(x => x.Created)
                .Where(x => x.TutorId == r.TutorId)
                .GroupBy(x => x.Created)
                .Select(x => x.Key)
                .ToDictionaryAsync(x => x.Id, x => x.FullName());
            return students;
        }
    }
}