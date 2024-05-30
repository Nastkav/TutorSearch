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
    public int? StudentId { get; set; }
    public int? TutorId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public class GetLessonsQueryHandler : BaseMediatrHandler<GetLessonsQuery, List<Lesson>>
    {
        public override async Task<List<Lesson>> Handle(GetLessonsQuery r, CancellationToken token)
        {
            //Підготовка 
            if (!r.TutorId.HasValue && !r.StudentId.HasValue)
                throw new CommandParameterException("Не вказані параметри запиту");

            if (!r.From.HasValue)
                r.From = DateTime.MinValue;
            if (!r.To.HasValue)
                r.To = DateTime.MaxValue;

            //Запит
            var dbLessons = await DatabaseContext.Lessons.AsNoTracking()
                .Include(x => x.Subject)
                .Include(x => x.Students)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Where(x => x.To > r.From || r.To > x.From)
                .Where(x =>
                    (r.StudentId.HasValue && x.Students.Any(s => s.Id == r.StudentId)) || //Взтяти всі записи як учня
                    (r.TutorId.HasValue && x.TutorId == r.TutorId)) //Додати всі записи як вчителя
                .ToListAsync();

            var lesList = Mapper.Map<List<Lesson>>(dbLessons);
            return lesList;
        }

        public GetLessonsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}