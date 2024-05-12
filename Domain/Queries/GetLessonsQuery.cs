using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetLessonsQuery : IRequest<List<Lesson>>
{
    public int? UserId { get; set; }
    public int? TutorId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public class GetLessonsQueryHandler : BaseMediatrHandler<GetLessonsQuery, List<Lesson>>
    {
        public override async Task<List<Lesson>> Handle(GetLessonsQuery r, CancellationToken token)
        {
            //Підготовка 
            if (!r.From.HasValue)
                r.From = DateTime.MinValue;
            if (!r.To.HasValue)
                r.To = DateTime.MaxValue;

            //Запит
            var dbLessons = await ApplicationDb.Lessons
                .Include(x => x.Subject)
                .Include(x => x.Students)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.To > r.From || r.To > x.From)
                .Where(x => !r.UserId.HasValue || x.Students.Any(s => s.Id == r.UserId) || //Взтяти всі записи учня
                            !r.TutorId.HasValue || x.TutorId == r.TutorId) //Взтяти всі записи вчителя
                .ToListAsync();

            var lesList = Mapper.Map<List<Lesson>>(dbLessons);
            for (var i = 0; i < lesList.Count; i++)
                lesList[i].StudentsIds = dbLessons[i].Students.Select(x => x.Id).ToList();
            return lesList;
        }

        public GetLessonsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}