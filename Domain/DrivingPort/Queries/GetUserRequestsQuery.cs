using System.Runtime.InteropServices.JavaScript;
using Domain.DrivingPort.Models;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class GetUserRequestsQuery : IRequest<List<LessonRequestDto>>
{
    public int? UserId { get; set; }
    public bool OnlyActive { get; set; }

    public class GetUserRequestsQueryHandler : BaseMediatrHandler<GetUserRequestsQuery, List<LessonRequestDto>>
    {
        public GetUserRequestsQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<List<LessonRequestDto>> Handle(GetUserRequestsQuery request,
            CancellationToken cancellationToken)
        {
            List<LessonRequestDto> listRequest = new();
            listRequest.Add(new LessonRequestDto()
            {
                Id = 1,
                Subject = "Maths",
                UserName = "Sasha Ivanov",
                TutorName = "Petr Manyli",
                UserComment = "Hi!1",
                TutorComment = "Hi Student!1",
                Answer = true,
                From = DateTime.Today.AddHours(8),
                To = DateTime.Today.AddHours(10)
            });
            listRequest.Add(new LessonRequestDto()
            {
                Id = 2,
                Subject = "Physics",
                UserName = "Sasha Ivanov",
                TutorName = "Petr Manyli",
                UserComment = "Hi!2",
                TutorComment = "Hi Student!2",
                Answer = true,
                From = DateTime.Today.AddHours(11),
                To = DateTime.Today.AddHours(12)
            });
            listRequest.Add(new LessonRequestDto()
            {
                Id = 3,
                Subject = "Maths",
                UserName = "Maria Kobykh",
                TutorName = "Svetlana Manyli",
                UserComment = "Hi!3",
                TutorComment = "Hi Student!3",
                Answer = true,
                From = DateTime.Today.AddHours(8),
                To = DateTime.Today.AddHours(10)
            });
            return listRequest;
            //TODO: GetUserRequestsQueryHandler
        }
    }
}