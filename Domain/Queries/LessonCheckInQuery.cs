using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Queries;

public class LessonCheckInCommand : IRequest<bool>
{
    public int UpdatedBy { get; set; }
    private readonly DateTime _createdAt;

    /// <summary>
    /// TODO
    /// </summary>
    public LessonCheckInCommand() => _createdAt = DateTime.Now;

    public class LessonCheckInCommandHandler : BaseMediatrHandler<LessonCheckInCommand, bool>
    {
        public override async Task<bool> Handle(LessonCheckInCommand request,
            CancellationToken cancellationToken) =>
            //TODO: LessonCheckInCommandHandler
            true;

        public LessonCheckInCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}