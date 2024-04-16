using Domain.DrivingPort.Models;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Commands;

public class CheckFavoriteCommand : IRequest<bool>
{
    public int? UserId { get; set; }
    public int TutorProfileId { get; set; }
    public bool Status { get; set; }

    public class CheckFavoriteCommandHandler : BaseMediatrHandler<CheckFavoriteCommand, bool>
    {
        public CheckFavoriteCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<bool> Handle(CheckFavoriteCommand request,
            CancellationToken cancellationToken) => true; //TODO: CheckFavoriteCommand
    }
}