using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetStudentTutorsQuery : IRequest<Dictionary<int, string>>
{
    public int StudentId { get; set; }

    public class GetStudentTutorsQueryHandler : BaseMediatrHandler<GetStudentTutorsQuery, Dictionary<int, string>>
    {
        public GetStudentTutorsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<Dictionary<int, string>> Handle(GetStudentTutorsQuery r, CancellationToken token)
        {
            //Вибір вчителів у яких були уроки зі студентом 
            var students = await DatabaseContext.Lessons.AsNoTracking()
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.Students.Any(s => s.Id == r.StudentId))
                .GroupBy(x => x.TutorId)
                .Select(x => x.Key)
                .Join(DatabaseContext.Users, x => x, u => u.Id, (_, u) => u)
                .ToDictionaryAsync(g => g.Id, g => g.FullName());
            return students;
        }
    }
}