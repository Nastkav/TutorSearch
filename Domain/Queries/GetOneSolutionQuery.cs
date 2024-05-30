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
            var dbTask = await DatabaseContext.Solutions.AsNoTracking()
                .Include(x => x.Student)
                .Include(x => x.Assignment)
                .ThenInclude(x => x.Tutor)
                .ThenInclude(x => x.User)
                .Include(x => x.Assignment.Subject)
                .Include(x => x.Files)
                .ThenInclude(x => x.Owner)
                .Include(x => x.Assignment.Files)
                .Where(x => x.StudentId == r.UserId || x.Assignment.TutorId == r.UserId)
                .FirstOrDefaultAsync(x => x.Id == r.SolutionId);

            if (dbTask == null)
                throw new SolutionException("Задачу не знайдено");

            var solution = Mapper.Map<Solution>(dbTask);
            return solution;
        }

        public GetOneSolutionQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}