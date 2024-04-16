using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Commands;

public class UpdateRequestCommand : IRequest<int>
{
    public string? Subject { get; set; }
    public int TutorProfileId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Comment { get; set; }
    public int UpdatedBy { get; set; }

    public class UpdateRequestCommandHandler : BaseMediatrHandler<UpdateRequestCommand, int>
    {
        public UpdateRequestCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<int> Handle(UpdateRequestCommand request,
            CancellationToken cancellationToken)
        {
            //TODO: UpdateRequestCommandHandler
            var id = 1;
            return id;
        }
    }
}