using System.ComponentModel;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Exceptions;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class UpdateRequestCommand : IRequest<int>
{
    [DisplayName("Запит №")] public int Id { get; set; }
    public string? Subject { get; set; }
    public int TutorId { get; set; }
    [DisplayName("Початок")] public DateTime? From { get; set; }
    [DisplayName("Кінець")] public DateTime? To { get; set; }
    [DisplayName("Коментар вчителя")] public string TutorComment { get; set; } = string.Empty;
    [DisplayName("Статус")] public CourseRequestStatus Status { get; set; } = CourseRequestStatus.New;
    public int UpdatedBy { get; set; }

    public class UpdateRequestCommandHandler : BaseMediatrHandler<UpdateRequestCommand, int>
    {
        public UpdateRequestCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(UpdateRequestCommand r, CancellationToken token)
        {
            if (r.UpdatedBy != r.TutorId)
                throw new IncorrectUserId("Тільки репетитор може оновлювати запити");

            var dbRequest = ApplicationDb.Requests
                                .Include(x => x.Subject)
                                .First(x => x.Id == r.Id && x.TutorId == r.TutorId)
                            ?? throw new Exception("Потрібний запит не знайдено");

            if (dbRequest.Status != CourseRequestStatus.New)
                throw new Exception("Заявка вже закрита");

            Mapper.Map<UpdateRequestCommand, RequestModel>(r, dbRequest);
            if (r.Subject != null)
                dbRequest.Subject = ApplicationDb.Subjects
                    .First(x => x.Name == r.Subject);
            ApplicationDb.Requests.Update(dbRequest);

            if (dbRequest.Status == CourseRequestStatus.Approved)
            {
                // var student = ApplicationDb.Users.First(x => x.Id == dbRequest.CreatedId);
                // //Course
                // var newCourse = new CourseModel
                // {
                //     Title = "", //TODO: dbRequest.Tutor.Created.FullName(),
                //     SubjectId = dbRequest.Subject.Id,
                //     RequestId = r.Id,
                //     TutorId = r.TutorId
                // };
                // newCourse.Students.Add(student);
                //
                // //Lesson
                // var firstLesson = Mapper.Map<RequestModel, LessonModel>(dbRequest);
                // firstLesson.CreatedId = r.UpdatedBy;
                // firstLesson.Students.Add(student);
                // firstLesson.TutorProfileId = dbRequest.TutorId;
                //
                // newCourse.Lessons.Add(firstLesson);
                // await ApplicationDb.Courses.AddAsync(newCourse);
                // // await ApplicationDb.Lessons.AddAsync(firstLesson);
            }

            await ApplicationDb.SaveChangesAsync();
            return dbRequest.Id;
        }
    }
}