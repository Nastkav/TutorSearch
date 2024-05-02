using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain.Entities;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateRequestCommand : IRequest<int>
{
    public string Subject { get; set; } = null!;
    public int CreatedBy { get; set; }
    public int TutorProfileId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    [MaxLength(300)] public string Comment { get; set; } = string.Empty;

    public class CreateRequestCommandHandler : BaseMediatrHandler<CreateRequestCommand, int>
    {
        public CreateRequestCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateRequestCommand request,
            CancellationToken cancellationToken)
        {
            var req = Mapper.Map<RequestModel>(request);
            req.Subject = ApplicationDb.Subjects.First(x => x.Name == request.Subject);
            req.Status = CourseRequestStatus.New;

            if (request.CreatedBy == request.TutorProfileId)
                throw new Exception("Користувач може надіслати запрошення лише іншому репетитору");

            var exist = ApplicationDb.Requests.Any(x =>
                x.CreatedId == req.CreatedId
                && x.Status == CourseRequestStatus.New
                && x.TutorId == req.TutorId
                && x.Subject.Id == req.Subject.Id);
            if (exist)
                throw new Exception("Ви вже маєте активний запит на курс для цього викладача");

            //Перевірка перетинання часу    
            var lessonOnRange = ApplicationDb.Lessons //TODO: Check this
                .Where(x => x.To > request.From || request.To > x.From).ToList();
            if (lessonOnRange.Count > 0)
                throw new Exception("Додавання неможливе, час перетинається");


            await ApplicationDb.Requests.AddAsync(req);
            await ApplicationDb.SaveChangesAsync();
            return req.Id;
        }
    }
}