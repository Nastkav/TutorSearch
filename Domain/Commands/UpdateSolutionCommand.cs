using System.ComponentModel.DataAnnotations;
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

public class UpdateSolutionCommand : IRequest<int>
{
    public int UpdatedBy { get; set; }
    public int SolutionId { get; set; }
    public SolutionStatus? Status { get; set; }
    public string? Answer { get; set; } = string.Empty;
    public string? TutorComment { get; set; } = string.Empty;

    public class UpdateSolutionCommandHandler : BaseMediatrHandler<UpdateSolutionCommand, int>
    {
        public override async Task<int> Handle(UpdateSolutionCommand r, CancellationToken token)
        {
            var dbSolution = DatabaseContext.Solutions
                .Include(x => x.Assignment)
                .FirstOrDefault(x => x.Id == r.SolutionId);
            if (dbSolution == null)
                throw new SolutionException("Задачу не знайдено");
            if (dbSolution.Status == SolutionStatus.Completed)
                throw new SolutionException("Оновленя виконаної задачі неможливо");
            var isStudent = r.UpdatedBy == dbSolution.StudentId;
            var isTutor = r.UpdatedBy == dbSolution.Assignment.TutorId;
            if (!isStudent && !isTutor)
                throw new AccessDeniedException("Редагувати завдання може лише вчитель або учень");

            //Status
            if (r.Status != null && dbSolution.Status != r.Status)
            {
                if (isStudent && r.Status.Value == SolutionStatus.Completed)
                    throw new AccessDeniedException("Учень не може відзначити завдання як виконане");
                dbSolution.Status = r.Status.Value;
            }

            //Answer
            if (r.Answer != null && dbSolution.Answer != r.Answer)
                if (isStudent)
                    dbSolution.Answer = r.Answer;
                else
                    throw new AccessDeniedException("Вчитель не може написати відповідь");


            //TutorComment
            if (r.TutorComment != null && dbSolution.TutorComment != r.TutorComment)
                if (isTutor)
                    dbSolution.TutorComment = r.TutorComment;
                else
                    throw new AccessDeniedException("Учень не може написати коментар");


            //Зберегти
            DatabaseContext.Solutions.Update(dbSolution);
            await DatabaseContext.SaveChangesAsync();
            return dbSolution.Id;
        }

        public UpdateSolutionCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}