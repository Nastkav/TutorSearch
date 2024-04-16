using Domain.DrivingPort.Models;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Commands;

public class UpdateLessonTimeCommand : IRequest<bool>
{
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }
    public LessonDto UserEvent { get; set; } = null!;

    public class UpdateLessonTimeCommandHandler : BaseMediatrHandler<UpdateLessonTimeCommand, bool>
    {
        public UpdateLessonTimeCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<bool> Handle(UpdateLessonTimeCommand request,
            CancellationToken cancellationToken) =>
            true; //TODO: UpdateLessonTimeCommandHandler
    }
}

public class DeleteLessonTimeCommand : IRequest<bool>
{
    public int UpdatedBy { get; set; }
    public int EventId { get; set; }

    public class DeleteLessonTimeCommandHandler : BaseMediatrHandler<DeleteLessonTimeCommand, bool>
    {
        public DeleteLessonTimeCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<bool> Handle(DeleteLessonTimeCommand request,
            CancellationToken cancellationToken) =>
            true; //TODO: DeleteLessonTimeCommandHandler
    }
}