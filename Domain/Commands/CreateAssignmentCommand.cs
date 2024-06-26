using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Commands;

public class CreateAssignmentCommand : IRequest<int>
{
    public int? CreatedId { get; set; }
    public Assignment Assignment { get; set; } = null!;

    public class CreateAssignmentCommandHandler : BaseMediatrHandler<CreateAssignmentCommand, int>
    {
        public CreateAssignmentCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<int> Handle(CreateAssignmentCommand r, CancellationToken token)
        {
            if (r.CreatedId != r.Assignment.TutorId)
                throw new CommandParameterException("Лише репетитор може додати завдання");
            if (!DatabaseContext.Tutors.Any(x => x.Id == r.Assignment.TutorId))
                throw new UserNotFoundException("Репетитор вказан невірно");

            var dbSubject = await DatabaseContext.Subjects
                .FirstOrDefaultAsync(x => x.Id == r.Assignment.SubjectId || x.Name == r.Assignment.SubjectName);
            if (dbSubject == null)
                throw new SubjectNotFoundException(
                    $"Предмету '{r.Assignment.SubjectId}:{r.Assignment.SubjectName}' не знайдено.");

            var newAssignment = Mapper.Map<AssignmentModel>(r.Assignment);
            newAssignment.Subject = dbSubject;

            //Додання рішень
            foreach (var studentId in r.Assignment.StudentsIds)
                newAssignment.Solutions.Add(new SolutionModel()
                {
                    Assignment = newAssignment,
                    StudentId = studentId,
                    Status = SolutionStatus.Todo
                });


            await DatabaseContext.Assignments.AddAsync(newAssignment);
            await DatabaseContext.SaveChangesAsync();
            return newAssignment.Id;
        }
    }
}