using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class LessonCloneCommand : IRequest<int>
{
    public int CreatedBy { get; set; }
    public int LessonId { get; set; }

    public class LessonCloneCommandHandler : BaseMediatrHandler<LessonCloneCommand, int>
    {
        public LessonCloneCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<int> Handle(LessonCloneCommand request,
            CancellationToken cancellationToken) =>
            //TODO: LessonCloneCommandHandler
            0;
    }
}