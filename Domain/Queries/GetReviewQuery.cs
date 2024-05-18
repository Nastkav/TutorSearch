using Domain.Models;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Queries;

public class GetReviewQuery : IRequest<Review>
{
    public int AuthorId { get; set; }
    public int TutorId { get; set; }

    public class GetReviewQueryHandler : BaseMediatrHandler<GetReviewQuery, Review>
    {
        public override async Task<Review> Handle(GetReviewQuery r, CancellationToken token)
        {
            if (r.AuthorId == 0 || r.TutorId == 0)
                throw new Exception("Ідентіфикатори невірні");

            //Запит
            var dbReview = await DatabaseContext.Reviews
                .Include(x => x.Author)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.TutorId == r.TutorId && r.AuthorId == x.AuthorId);

            // Повернути новій якщо відгук не знайдено
            Review review;
            if (dbReview == null)
            {
                review = new Review() { AuthorId = r.AuthorId, TutorId = r.TutorId };
                review.Rating = 10;
                review.TutorName = await DatabaseContext.Users
                    .Where(x => x.Id == r.TutorId)
                    .Select(x => x.FullName()).FirstAsync();
                review.AuthorName = await DatabaseContext.Users
                    .Where(x => x.Id == r.AuthorId)
                    .Select(x => x.FullName()).FirstAsync();
            }
            else
            {
                review = Mapper.Map<Review>(dbReview);
            }

            return review;
        }

        public GetReviewQueryHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}