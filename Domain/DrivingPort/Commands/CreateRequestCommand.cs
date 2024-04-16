using System.ComponentModel.DataAnnotations;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Commands;

public class CreateRequestCommand : IRequest<int>
{
    public string Subject { get; set; } = null!;
    public int CreatedBy { get; set; }
    public int TutorProfileId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Comment { get; set; } = "";

    public class CreateRequestCommandHandler : BaseMediatrHandler<CreateRequestCommand, int>
    {
        public CreateRequestCommandHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<int> Handle(CreateRequestCommand request,
            CancellationToken cancellationToken)
        {
            //TODO: CreateRequestCommandHandler
            var id = 1;
            return id;
        }
    }
}