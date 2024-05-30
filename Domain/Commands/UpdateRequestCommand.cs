using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class UpdateRequestCommand : IRequest<int>
{
    [DisplayName("Запит №")] public int Id { get; set; }
    [StringLength(150)] public string? Subject { get; set; }
    public int TutorId { get; set; }
    [DisplayName("Початок")] public DateTime? From { get; set; }
    [DisplayName("Кінець")] public DateTime? To { get; set; }

    [DisplayName("Коментар вчителя")]
    [StringLength(254, ErrorMessage = "{0} має містити не більше {1} символів.", MinimumLength = 0)]
    public string TutorComment { get; set; } = string.Empty;

    [DisplayName("Статус")] public LessonRequestStatus Status { get; set; } = LessonRequestStatus.New;
    public int UpdatedBy { get; set; }

    public class UpdateRequestCommandHandler : BaseMediatrHandler<UpdateRequestCommand, int>
    {
        public UpdateRequestCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<int> Handle(UpdateRequestCommand r, CancellationToken token)
        {
            if (r.UpdatedBy != r.TutorId)
                throw new RequestException("Тільки репетитор може оновлювати запити");

            var dbRequest = DatabaseContext.Requests
                .Include(x => x.Subject)
                .Include(x => x.Created)
                .FirstOrDefault(x => x.Id == r.Id && x.TutorId == r.TutorId);
            if (dbRequest is null)
                throw new RequestException("Потрібний запит не знайдено");

            if (dbRequest.Status != LessonRequestStatus.New)
                throw new RequestException("Заявка вже закрита");

            Mapper.Map(r, dbRequest);
            if (r.Subject != null)
                dbRequest.Subject = DatabaseContext.Subjects
                    .First(x => x.Name == r.Subject);
            DatabaseContext.Requests.Update(dbRequest);

            if (dbRequest.Status == LessonRequestStatus.Approved) // Створення нового уроку
            {
                var lesson = Mapper.Map<LessonModel>(dbRequest);
                lesson.Students.Add(dbRequest.Created);
                await DatabaseContext.Lessons.AddAsync(lesson);
            }

            await DatabaseContext.SaveChangesAsync();
            return dbRequest.Id;
        }
    }
}