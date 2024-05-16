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

    [DisplayName("Статус вирішення задачі")]
    public SolutionStatus? Status { get; set; }

    [DisplayName("Вчитель")] public int? StudentId { get; set; }

    [DisplayName("Учень")] public int? TutorId { get; set; }

    [DisplayName("Задача")] public int? AssignmentId { get; set; }

    public class GetSolutionsQueryHandler : BaseMediatrHandler<GetSolutionsQuery, List<Solution>>
    {
        public override async Task<List<Solution>> Handle(GetSolutionsQuery r, CancellationToken token)
        {
            //Запит
            var q = ApplicationDb.Solutions
                .Include(x => x.Student)
                .Include(x => x.Assignment)
                .ThenInclude(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Include(x => x.Assignment.Subject)
                .Include(x => x.Files).AsQueryable();

            if (r.Status != null)
                q = q.Where(x => x.Status == r.Status);
            if (r.StudentId != null)
                q = q.Where(x => x.StudentId == r.StudentId);
            if (r.TutorId != null)
                q = q.Where(x => x.Assignment.TutorId == r.TutorId);
            if (r.Status != null)
                q = q.Where(x => x.Assignment.Id == r.AssignmentId);

            var dbSolutions = await q.ToListAsync();
            var solutions = Mapper.Map<List<Solution>>(dbSolutions);
            return solutions;
        }

        public GetSolutionsQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}