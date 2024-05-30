using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class GetSolutionsQuery : IRequest<List<Solution>>
{
    public int UserId { get; set; }

    [DisplayName("Статус задачі")] public List<SolutionStatus> Status { get; set; } = [];

    [DisplayName("Предмет")] public List<int> SubjectId { get; set; } = [];
    [DisplayName("Учень")] public List<int> StudentId { get; set; } = [];
    [DisplayName("Вчитель")] public List<int> TutorId { get; set; } = [];
    [DisplayName("Задача")] public List<int> AssignmentId { get; set; } = [];

    public class GetSolutionsQueryHandler : BaseMediatrHandler<GetSolutionsQuery, List<Solution>>
    {
        public override async Task<List<Solution>> Handle(GetSolutionsQuery r, CancellationToken token)
        {
            //Запит
            var q = DatabaseContext.Solutions.AsNoTracking()
                .Include(x => x.Student)
                .Include(x => x.Assignment)
                .ThenInclude(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Include(x => x.Assignment.Subject)
                .Include(x => x.Files)
                .ThenInclude(x => x.Owner)
                .Where(x => x.StudentId == r.UserId || x.Assignment.TutorId == r.UserId)
                .AsQueryable();
            //Filters
            if (r.SubjectId.Count > 0)
                q = q.Where(x => r.SubjectId.Contains(x.Assignment.SubjectId));
            if (r.Status.Count > 0)
                q = q.Where(x => r.Status.Contains(x.Status));
            if (r.StudentId.Count > 0)
                q = q.Where(x => r.StudentId.Contains(x.StudentId));
            if (r.TutorId.Count > 0)
                q = q.Where(x => r.TutorId.Contains(x.Assignment.TutorId));
            if (r.AssignmentId.Count > 0)
                q = q.Where(x => r.AssignmentId.Contains(x.AssignmentId));

            //Execute
            var dbSolutions = await q.ToListAsync();
            var solutions = Mapper.Map<List<Solution>>(dbSolutions);
            return solutions;
        }

        public GetSolutionsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}