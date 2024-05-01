using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Queries;

public class LessonCloneCommand : IRequest<int>
{
    public int CreatedBy { get; set; }
    public int LessonId { get; set; }

    public class LessonCloneCommandHandler : BaseMediatrHandler<LessonCloneCommand, int>
    {
        public LessonCloneCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }


        public override async Task<int> Handle(LessonCloneCommand request,
            CancellationToken cancellationToken) =>
            //TODO: LessonCloneCommandHandler
            0;
    }
}