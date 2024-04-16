using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort;

public abstract class BaseMediatrHandler<T, TU> : IRequestHandler<T, TU>
    where T : IRequest<TU>
{
    protected internal IEventRepository EventRepo { get; }
    protected internal IUserRepository UserRepo { get; }

    public BaseMediatrHandler(IEventRepository eventRepo, IUserRepository userRepo)
    {
        EventRepo = eventRepo;
        UserRepo = userRepo;
    }
    // _dbContext = dbContext;
    // Logger = logger;

    public abstract Task<TU> Handle(T request, CancellationToken cancellationToken);
}