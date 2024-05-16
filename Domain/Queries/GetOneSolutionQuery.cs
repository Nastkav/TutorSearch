using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetOneSolutionQuery : IRequest<Solution?>
{
    public int SolutionId { get; set; }
    public int UserId { get; set; }

    public class GetOneSolutionQueryHandler : BaseMediatrHandler<GetOneSolutionQuery, Solution?>
    {
        public override async Task<Solution?> Handle(GetOneSolutionQuery r, CancellationToken token)
        {
            //Запит
            var dbTask = await ApplicationDb.Solutions
                .Include(x => x.Student)
                .Include(x => x.Assignment)
                .ThenInclude(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Include(x => x.Assignment.Subject)
                .Include(x => x.Files)
                .Where(x => x.StudentId == r.UserId || x.Assignment.TutorId == r.UserId)
                .FirstOrDefaultAsync(x => x.Id == r.SolutionId);

            if (dbTask == null)
                throw new Exception("Задачу не знайдено");

            var Solution = Mapper.Map<Solution>(dbTask);
            return Solution;
        }

        public GetOneSolutionQueryHandler(ILoggerFactory loggerFactory, AppDbContext dbContext, IMapper mapper)
            : base(loggerFactory, dbContext, mapper)
        {
        }
    }
}