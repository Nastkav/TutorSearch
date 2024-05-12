using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateLessonCommand : IRequest<int>
{
    public int CreatedId { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public class CreateRequestCommandHandler : BaseMediatrHandler<CreateLessonCommand, int>
    {
        public CreateRequestCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateLessonCommand r, CancellationToken token)
        {
            if (r.CreatedId != r.Lesson.TutorId)
                throw new Exception("Лише репетитор може створити зустріч");
            if (!ApplicationDb.Tutor.Any(x => x.Id == r.Lesson.TutorId))
                throw new Exception("Репетитор вказан невірно");

            var dbSubject = await ApplicationDb.Subjects
                .FirstOrDefaultAsync(x => x.Id == r.Lesson.SubjectId || x.Name == r.Lesson.SubjectName);
            if (dbSubject == null)
                throw new Exception($"Предмету '{r.Lesson.SubjectId}:{r.Lesson.SubjectName}' не знайдено.");

            var newLesson = Mapper.Map<LessonModel>(r.Lesson);
            newLesson.Subject = dbSubject;

            //Дадання учнів
            newLesson.Students = ApplicationDb.Users.Where(x => r.Lesson.StudentsIds.Contains(x.Id)).ToList();

            //Перевірка перетинання часу    
            //aF > bT and bF > aT
            if (await ApplicationDb.Lessons.CountAsync(x => x.From > r.Lesson.To && r.Lesson.From > x.To) > 0)
                throw new Exception("У вас вже є зустріч у цей час");

            await ApplicationDb.Lessons.AddAsync(newLesson);
            await ApplicationDb.SaveChangesAsync();
            return newLesson.Id;
        }
    }
}