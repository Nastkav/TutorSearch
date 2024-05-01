using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infra.DatabaseAdapter.Models;

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

        public override async Task<int> Handle(UpdateRequestCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UpdatedBy == request.TutorProfileId)
                throw new IncorrectUserId("A user can only send an invitation to another tutor");

            var dbRequest = ApplicationDb.Requests.First(x =>
                                x.CreatedBy == request.UpdatedBy
                                && x.TutorId == request.TutorProfileId
                                && x.Id == request.Id)
                            ?? throw new Exception("The required query was not found");

            if (dbRequest.Status != CourseRequestStatus.New)
                throw new Exception("The request is already closed");

            Mapper.Map(request, dbRequest);
            if (request.Subject != null)
                dbRequest.Subject = ApplicationDb.Subjects.First(x => x.Name == request.Subject);
            ApplicationDb.Requests.Update(dbRequest);
            await ApplicationDb.SaveChangesAsync();

            if (dbRequest.Status == CourseRequestStatus.Approved)
                Log.LogInformation("Create Course"); //TODO: Create Course

            return dbRequest.Id;
        }
    }
}