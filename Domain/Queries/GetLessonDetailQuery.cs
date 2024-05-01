using Domain.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Domain.Queries;

public class GetLessonDetailQuery : IRequest<LessonDetailsDto>
{
    public int UserId { get; set; }
    public int LessonId { get; set; }

    public class GetLessonDetailQueryHandler : BaseMediatrHandler<GetLessonDetailQuery, LessonDetailsDto>
    {
        public GetLessonDetailQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }


        public override async Task<LessonDetailsDto> Handle(GetLessonDetailQuery request,
            CancellationToken cancellationToken)
        {
            //TODO: GetLessonDetailQueryHandler
            List<LessonDto> events = new();
            var dtMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            return new LessonDetailsDto()
            {
                Type = TimeType.Busy,
                Title = "Maths -- Petrov",
                StartTime = dtMonday.AddHours(10),
                EndTime = dtMonday.AddHours(12),
                Subject = "Maths",
                Comment = "https://link.to/AAAAAAAAAAAAAAA",
                CourseId = Guid.NewGuid().ToString(),
                CourseName = "German Pre-intermediate"
            };
        }
    }
}