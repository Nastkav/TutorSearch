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
    public DateTime? Deadline { get; set; }
    public int? SubjectId { get; set; }
    public List<int> StudentIds { get; set; } = [];


    public class UpdateAssignmentCommandHandler : BaseMediatrHandler<UpdateAssignmentCommand, Assignment>
    {
        public override async Task<Assignment> Handle(UpdateAssignmentCommand r, CancellationToken token)
        {
            var dbAssignment = DatabaseContext.Assignments
                .Include(x => x.Solutions)
                .FirstOrDefault(x => x.Id == r.AssignmentId);
            if (dbAssignment == null)
                throw new AssignmentException("Задачу не знайдено");
            if (r.UpdatedBy == 0)
                throw new UserNotFoundException("Користувача не вказано");
            if (r.UpdatedBy != dbAssignment.TutorId)
                throw new CommandParameterException("Редагувати завдання може лише вчитель");
            if (r.Deadline.HasValue && r.Deadline < DateTime.Today)
                throw new TimeRangeException("Термін має бути у майбутньому.", r.Deadline.Value);

            if (r.Title != null) dbAssignment.Title = r.Title;
            if (r.Description != null) dbAssignment.Description = r.Description;
            if (r.Deadline != null) dbAssignment.Deadline = r.Deadline.Value;
            if (r.SubjectId != null) dbAssignment.SubjectId = r.SubjectId.Value;

            //Видалити рішення які ще не виконані та їх немає у списку 
            foreach (var s in dbAssignment.Solutions)
                if (!r.StudentIds.Contains(s.StudentId) && s.Status == SolutionStatus.Todo)
                    DatabaseContext.Solutions.Remove(s);

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
            DatabaseContext.Assignments.Update(dbAssignment);
            await DatabaseContext.SaveChangesAsync();
            //Повернути оновлене завдання
            var task = Mapper.Map<Assignment>(dbAssignment);
            return task;
        }

        public UpdateAssignmentCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}