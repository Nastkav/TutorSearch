using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetOneLessonQuery : IRequest<Lesson?>
{
    public int LessonId { get; set; }
    public int UserId { get; set; }

    public class GetOneLessonQueryHandler : BaseMediatrHandler<GetOneLessonQuery, Lesson?>
    {
        public override async Task<Lesson?> Handle(GetOneLessonQuery r, CancellationToken token)
        {
            //Запит
            var dbLesson = await ApplicationDb.Lessons
                .Include(x => x.Students)
                .Include(x => x.Subject)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x =>
                    (x.TutorId == r.UserId || x.Students.Any(s => s.Id == r.UserId))
                    && x.Id == r.LessonId);
            if (dbLesson == null)
                return null;

            var lesson = Mapper.Map<Lesson>(dbLesson);
            return lesson;
        }

        public GetOneLessonQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}