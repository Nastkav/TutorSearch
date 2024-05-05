using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
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
    [DisplayName("Коментар вчителя")]
    public string Comment { get; set; } = string.Empty;


    public class CreateRequestCommandHandler : BaseMediatrHandler<CreateRequestCommand, int>
    {
        public CreateRequestCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateRequestCommand r, CancellationToken token)
        {
            if (r.CreatedId == r.TutorId)
                throw new Exception("Користувач може надіслати запрошення лише іншому репетитору");
            if (!ApplicationDb.TutorProfiles.Any(x => x.Id == r.TutorId))
                throw new Exception("Репетитор вказан невірно");
            if (!ApplicationDb.Users.Any(x => x.Id == r.CreatedId))
                throw new Exception("Ученик вказан невірно");

            var dbSubject = await ApplicationDb.Subjects.FirstOrDefaultAsync(x => x.Id == r.SubjectId);
            if (dbSubject == null)
                throw new Exception($"Предмету '{r.SubjectId}' не знайдено.");

            var newRequest = Mapper.Map<RequestModel>(r);
            newRequest.Subject = dbSubject;
            newRequest.Status = CourseRequestStatus.New;

            //Перевірка існування схожого запиту до цього викладача    
            var requestExist = ApplicationDb.Requests.Any(x =>
                x.CreatedId == newRequest.CreatedId &&
                x.Status == newRequest.Status &&
                x.TutorId == newRequest.TutorId &&
                x.Subject.Id == newRequest.Subject.Id
            );
            if (requestExist)
                throw new Exception("Ви вже маєте активний запит на курс для цього викладача");

            //Перевірка перетинання часу    
            if (await ApplicationDb.Lessons.CountAsync(x => x.To > newRequest.From || newRequest.To > x.From) > 0)
                throw new Exception("Додавання неможливе, час перетинається");

            await ApplicationDb.Requests.AddAsync(newRequest);
            await ApplicationDb.SaveChangesAsync();
            return newRequest.Id;
        }
    }
}