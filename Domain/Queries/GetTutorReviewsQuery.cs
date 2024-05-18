using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetTutorReviewsQuery : IRequest<List<Review>>
{
    public int TutorId { get; set; }

    public class GetTutorReviewsQueryHandler : BaseMediatrHandler<GetTutorReviewsQuery, List<Review>>
    {
        public override async Task<List<Review>> Handle(GetTutorReviewsQuery r, CancellationToken token)
        {
            if (r.TutorId == 0)
                throw new Exception("Ідентіфикатори невірні");

            //Запит
            var dbReview = await DatabaseContext.Reviews
                .Include(x => x.Tutor)
                .Include(x => x.Author)
                .Where(x => x.TutorId == r.TutorId).ToListAsync();

            var reviews = Mapper.Map<List<Review>>(dbReview);
            return reviews;
        }

        public GetTutorReviewsQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}