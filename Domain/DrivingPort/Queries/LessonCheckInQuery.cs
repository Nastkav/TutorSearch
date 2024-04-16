using Domain.DrivingPort.Models;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class LessonCheckInCommand : IRequest<bool>
{
    public int UpdatedBy { get; set; }
    private readonly DateTime _createdAt;

    public LessonCheckInCommand() => _createdAt = DateTime.Now;

    public class LessonCheckInCommandHandler : BaseMediatrHandler<LessonCheckInCommand, bool>
    {
        public LessonCheckInCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<bool> Handle(LessonCheckInCommand request,
            CancellationToken cancellationToken) =>
            //TODO: LessonCheckInCommandHandler
            true;
    }
}