using AutoMapper;
using Domain.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateLessonTimeCommand : IRequest<int>
{
    public int CreatedBy { get; set; }

    public TimeType Type { get; set; }
    public string Title { get; set; } = "";
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public Guid? CourseId { get; set; }


    public class CreateLessonTimeCommandHandler : BaseMediatrHandler<CreateLessonTimeCommand, int>
    {
        public CreateLessonTimeCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateLessonTimeCommand request, CancellationToken cancellationToken)
        {
            //On day event
            if (request.From.Date != request.To.Date)
                throw new Exception("���� �� ���� �������� ���.");
            var weekday = (int)request.From.DayOfWeek;
            var start = TimeOnly.FromDateTime(request.From);
            var end = TimeOnly.FromDateTime(request.To);


            switch (request.Type)
            {
                case TimeType.Available: //������������ �������� ���� ���������� � ������
                    var dbTimes = ApplicationDb.AvailableTimes
                        .Where(x => x.ProfileId == request.CreatedBy && x.DayOfWeek == weekday)
                        .OrderBy(x => x.StartTime)
                        .ToList();

                    //�������� ����������� ����
                    foreach (var timeRange in dbTimes)
                        if (timeRange.EndTime > start && timeRange.StartTime < end)
                            throw new Exception("��������� ���������, ��� ������������");
                    


                    var availableTime = new AvailableTime()
                    {
                        DayOfWeek = weekday,
                        StartTime = TimeOnly.FromDateTime(request.From),
                        EndTime = TimeOnly.FromDateTime(request.To),
                        ProfileId = request.CreatedBy,
                        CreatedId = request.CreatedBy
                    };
                    ApplicationDb.AvailableTimes.Add(availableTime);
                    await ApplicationDb.SaveChangesAsync();
                    return availableTime.Id;
                case TimeType.Busy: //��������� ������ �������
                    if (request.CourseId == null)
                        throw new Exception("������������ ������������� �����");
                    var students = ApplicationDb.Courses
                        .Where(x => x.Id == request.CourseId)
                        .Select(x => x.Students)
                        .FirstOrDefault();
                    if (students == null || students.Count == 0)
                        throw new Exception("������� � ������������� ����� ��� ������� ����");

                    //�������� �� ������� �� ������ � ��������� ���������
                    var availableRange = ApplicationDb.AvailableTimes
                        .Where(x => x.StartTime <= start && end <= x.EndTime && x.DayOfWeek == weekday)
                        .FirstOrDefault();
                    if (availableRange == null)
                        throw new Exception("������� ���� ������� ���");

                    //�������� ����������� ����
                    //https://scicomp.stackexchange.com/questions/26258/the-easiest-way-to-find-intersection-of-two-intervals
                    var lessonOnRange = ApplicationDb.Lessons
                        .Where(x => x.To > request.From || request.To > x.From).ToList();
                    if (lessonOnRange.Count > 0)
                        throw new Exception("��������� ���������, ��� ������������");

                    //�������
                    var newLesson = new LessonModel()
                    {
                        TutorProfileId = request.CreatedBy,
                        CourseId = request.CourseId.Value,
                        Students = students
                    };

                    ApplicationDb.Lessons.Add(newLesson);
                    await ApplicationDb.SaveChangesAsync();
                    return newLesson.Id;
                default:
                    throw new ArgumentOutOfRangeException("��������� ������ ���� ����: " + request.Type.ToString());
            }
        }
    }
}