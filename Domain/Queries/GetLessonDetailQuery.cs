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


        public override async Task<LessonDetailsDto> Handle(GetLessonDetailQuery r, CancellationToken token)
        {
            var dbLesson = ApplicationDb.Lessons.FirstOrDefault(x => x.Id == r.LessonId && x.CreatedId == r.UserId);
            if (dbLesson == null)
                throw new Exception("Обраний урок не знайдено");
            var lessonDto = Mapper.Map<LessonDetailsDto>(dbLesson);
            lessonDto.Type = TimeType.Busy;
            return lessonDto;
        }
    }
}