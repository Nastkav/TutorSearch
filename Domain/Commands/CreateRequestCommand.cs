using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateRequestCommand : IRequest<int>
{
    public int CreatedId { get; set; }
    public int TutorId { get; set; }
    [DisplayName("Початок")] public DateTime? From { get; set; }
    [DisplayName("Кінець")] public DateTime? To { get; set; }


    [Range(1, int.MaxValue, ErrorMessage = "Оберіть предмет")]
    public int? SubjectId { get; set; }

    public List<SelectListItem> Subjects { get; set; } = [];

    [MaxLength(300)]
    [MinLength(0)]
    [DisplayName("Коментар учня")]
    public string Comment { get; set; } = string.Empty;


    public class CreateRequestCommandHandler : BaseMediatrHandler<CreateRequestCommand, int>
    {
        public CreateRequestCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateRequestCommand r, CancellationToken token)
        {
            if (r.CreatedId == r.TutorId)
                throw new Exception("Користувач може надіслати запрошення лише іншому репетитору");
            if (!DatabaseContext.Tutor.Any(x => x.Id == r.TutorId))
                throw new Exception("Репетитор вказан невірно");
            if (!DatabaseContext.Users.Any(x => x.Id == r.CreatedId))
                throw new Exception("Ученик вказан невірно");

            var dbSubject = await DatabaseContext.Subjects.FirstOrDefaultAsync(x => x.Id == r.SubjectId);
            if (dbSubject == null)
                throw new Exception($"Предмету '{r.SubjectId}' не знайдено.");

            var newRequest = Mapper.Map<RequestModel>(r);
            newRequest.Subject = dbSubject;
            newRequest.Status = LessonRequestStatus.New;

            //Перевірка існування схожого запиту до цього викладача    
            var requestExist = DatabaseContext.Requests.Any(x =>
                x.CreatedId == newRequest.CreatedId &&
                x.Status == newRequest.Status &&
                x.TutorId == newRequest.TutorId &&
                x.Subject.Id == newRequest.Subject.Id
            );
            if (requestExist)
                throw new Exception("Ви вже маєте активний запит на курс для цього викладача");

            //Перевірка перетинання часу    
            //aF > bT and bF > aT
            if (await DatabaseContext.Lessons.CountAsync(x => x.From > newRequest.To && newRequest.From > x.To) > 0)
                throw new Exception("Додавання неможливе, час перетинається");

            await DatabaseContext.Requests.AddAsync(newRequest);
            await DatabaseContext.SaveChangesAsync();
            return newRequest.Id;
        }
    }
}