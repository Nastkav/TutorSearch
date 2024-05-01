using AutoMapper;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain;

public abstract class BaseMediatrHandler<T, TU> : IRequestHandler<T, TU>
    where T : IRequest<TU>
{
    protected internal ILogger Log { get; }
    protected internal AppDbContext ApplicationDb { get; }
    protected internal IMapper Mapper { get; }

    public BaseMediatrHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
    {
        Log = loggerFactory.CreateLogger<T>();
        ApplicationDb = dbContext;
        Mapper = mapper;
    }

    public abstract Task<TU> Handle(T request, CancellationToken cancellationToken);
}