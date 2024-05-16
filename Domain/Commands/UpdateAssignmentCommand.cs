using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class UpdateAssignmentCommand : IRequest<Assignment>
{
    public int UpdatedBy { get; set; }
    public int AssignmentId { get; set; }
    public string? Title { get; set; } = null;
    public string? Description { get; set; } = null;
    public DateOnly? Deadline { get; set; }
    public int? SubjectId { get; set; }
    public List<int> StudentIds { get; set; } = [];
    //TODO public IFormFile? AttachmentFile { get; set; }


    public class UpdateAssignmentCommandHandler : BaseMediatrHandler<UpdateAssignmentCommand, Assignment>
    {
        public override async Task<Assignment> Handle(UpdateAssignmentCommand r, CancellationToken token)
        {
            var dbAssignment = ApplicationDb.Assignments
                .Include(x => x.Solutions)
                .FirstOrDefault(x => x.Id == r.AssignmentId);
            if (dbAssignment == null)
                throw new Exception("Задачу не знайдено");
            if (r.UpdatedBy != dbAssignment.TutorId)
                throw new Exception("Редагувати завдання може лише вчитель");
            if (r.Deadline < DateOnly.FromDateTime(DateTime.Today))
                throw new Exception("Термін має бути у майбутньому.");

            if (r.Title != null) dbAssignment.Title = r.Title;
            if (r.Description != null) dbAssignment.Description = r.Description;
            if (r.Deadline != null) dbAssignment.Deadline = r.Deadline.Value.ToDateTime(TimeOnly.MinValue);
            if (r.SubjectId != null) dbAssignment.SubjectId = r.SubjectId.Value;

            //  StudentSolutions
            //  k - UserId, v - SolutionId

            //Видалити рішення які ще не виконані та їх немає у списку 
            foreach (var s in dbAssignment.Solutions)
                if (!r.StudentIds.Contains(s.StudentId) && s.Status == SolutionStatus.Todo)
                    ApplicationDb.Solutions.Remove(s);

            var studentsIds = dbAssignment.Solutions.Select(x => x.StudentId).ToList();
            //Видалення існуючих рішень студентів зі списку вставки
            foreach (var studId in studentsIds)
                r.StudentIds.Remove(studId);

            //Додати нові рішення рішення 
            foreach (var studentId in r.StudentIds)
                dbAssignment.Solutions.Add(new SolutionModel()
                {
                    AssignmentId = dbAssignment.Id,
                    StudentId = studentId,
                    Status = SolutionStatus.Todo
                });

            //Зберегти
            ApplicationDb.Assignments.Update(dbAssignment);
            await ApplicationDb.SaveChangesAsync();
            //Повернути оновлене завдання
            var task = Mapper.Map<Assignment>(dbAssignment);
            return task;
        }

        public UpdateAssignmentCommandHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}