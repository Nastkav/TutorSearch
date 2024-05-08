using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetAllSubjectsQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllSubjectsQueryHandler : BaseMediatrHandler<GetAllSubjectsQuery, Dictionary<int, string>>
    {
        public GetAllSubjectsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetAllSubjectsQuery r, CancellationToken token) =>
            ApplicationDb.Subjects.ToDictionary(k => k.Id, v => v.Name);
    }
}

public class GetTutorStudentsQuery : IRequest<Dictionary<int, string>>
{
    public int TutorId { get; set; }

    public class GetTutorStudentsQueryHandler : BaseMediatrHandler<GetTutorStudentsQuery, Dictionary<int, string>>
    {
        public GetTutorStudentsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetTutorStudentsQuery r, CancellationToken token)
        {
            //Вибір усіх студентів, у яких був хоча б один урок з репетитором
            var students = ApplicationDb.Users.Include(x => x.Lessons)
                .Where(x => x.Lessons.Any(y => y.TutorId == r.TutorId))
                .ToDictionary(x => x.Id, x => $"{x.Name} {x.Surname}({x.Email})");
            return students;
        }
    }
}