using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class UpdateRequestCommand : IRequest<int>
{
    public int Id { get; set; }
    public string? Subject { get; set; }
    public int TutorProfileId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Comment { get; set; }
    public int UpdatedBy { get; set; }
    public CourseRequestStatus Status { get; set; }

    public class UpdateRequestCommandHandler : BaseMediatrHandler<UpdateRequestCommand, int>
    {
        public UpdateRequestCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }

        public override async Task<int> Handle(UpdateRequestCommand r, CancellationToken token)
        {
            if (r.UpdatedBy == r.TutorProfileId)
                throw new IncorrectUserId("A user can only send an invitation to another tutor");

            var dbRequest = ApplicationDb.Requests
                                .Include(x => x.Tutor.Created)
                                .Include(x => x.Subject)
                                .First(x =>
                                    x.CreatedId == r.UpdatedBy
                                    && x.TutorId == r.TutorProfileId
                                    && x.Id == r.Id)
                            ?? throw new Exception("The required query was not found");

            if (dbRequest.Status != CourseRequestStatus.New)
                throw new Exception("The request is already closed");

            Mapper.Map(r, dbRequest);
            if (r.Subject != null)
                dbRequest.Subject = ApplicationDb.Subjects
                    .First(x => x.Name == r.Subject);
            ApplicationDb.Requests.Update(dbRequest);

            if (dbRequest.Status == CourseRequestStatus.Approved)
            {
                var student = ApplicationDb.Users.First(x => x.Id == dbRequest.CreatedId);
                //Course
                var newCourse = new CourseModel
                {
                    Title = dbRequest.Tutor.Created.FullName(),
                    SubjectId = dbRequest.Subject.Id,
                    RequestId = r.Id,
                    TutorId = r.TutorProfileId
                };
                newCourse.Students.Add(student);

                //Lesson
                var firstLesson = Mapper.Map<RequestModel, LessonModel>(dbRequest);
                firstLesson.CreatedId = r.UpdatedBy;
                firstLesson.Students.Add(student);
                firstLesson.TutorProfileId = dbRequest.TutorId;

                newCourse.Lessons.Add(firstLesson);
                await ApplicationDb.Courses.AddAsync(newCourse);
                // await ApplicationDb.Lessons.AddAsync(firstLesson);
            }

            await ApplicationDb.SaveChangesAsync();
            return dbRequest.Id;
        }
    }
}