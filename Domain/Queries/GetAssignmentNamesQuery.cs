using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetAssignmentNamesQuery : IRequest<Dictionary<int, string>>
{
    public int? TutorId { get; set; }
    public int? StudentId { get; set; }

    public class GetAssignmentNamesQueryHandler : BaseMediatrHandler<GetAssignmentNamesQuery, Dictionary<int, string>>
    {
        public override async Task<Dictionary<int, string>> Handle(GetAssignmentNamesQuery r, CancellationToken token)
        {
            if (!r.TutorId.HasValue && !r.StudentId.HasValue)
                throw new CommandParameterException("Не вказані параметри запиту");

            var q = DatabaseContext.Assignments.AsNoTracking()
                .Include(x => x.Solutions)
                .Where(x =>
                        (r.TutorId.HasValue && r.TutorId == x.TutorId) || //Пошук завдань як вчителя
                        (r.StudentId.HasValue && x.Solutions.Any(s => s.StudentId == r.StudentId)) //Пошук як учня
                ).AsQueryable();
            return await q.ToDictionaryAsync(k => k.Id, v => v.Title);
        }

        public GetAssignmentNamesQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}