using AutoMapper;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Helpers;

public abstract class BaseMediatrHandler<T, TU> : IRequestHandler<T, TU>
    where T : IRequest<TU>
{
    protected internal AppDbContext DatabaseContext { get; }
    protected internal IMapper Mapper { get; }

    public BaseMediatrHandler(AppDbContext dbContext, IMapper mapper)
    {
        DatabaseContext = dbContext;
        Mapper = mapper;
    }

    public abstract Task<TU> Handle(T r, CancellationToken token);
}