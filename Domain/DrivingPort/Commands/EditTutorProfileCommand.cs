using Domain.DrivingPort.Models;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Commands;

public class EditTutorProfileCommand : IRequest<bool>
{
    public int UpdatedBy { get; set; }
    public TutorEditDto TutorEdit { get; set; } = null!;


    public class CreateRequestCommandHandler : BaseMediatrHandler<EditTutorProfileCommand, bool>
    {
        public CreateRequestCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<bool> Handle(EditTutorProfileCommand request,
            CancellationToken cancellationToken) =>
            true; //TODO: CreateRequestCommandHandler
    }
}