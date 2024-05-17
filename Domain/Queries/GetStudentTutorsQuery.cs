using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            var students = await DatabaseContext.Lessons
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.Students.Any(s => s.Id == r.StudentId))
                .GroupBy(x => x.TutorId)
                .ToDictionaryAsync(g => g.Key, g => g.First().Tutor.User.FullName());
            return students;
        }
    }
}