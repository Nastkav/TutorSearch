using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands;

public class AddOrUpdateReviewCommand : IRequest<Review>
{
    public Review Review { get; set; } = null!;

    public class AddOrUpdateReviewCommandHandler : BaseMediatrHandler<AddOrUpdateReviewCommand, Review>
    {
        public AddOrUpdateReviewCommandHandler(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public override async Task<Review> Handle(AddOrUpdateReviewCommand r, CancellationToken token)
        {
            //Перевірка логіки
            if (r.Review.AuthorId == r.Review.TutorId)
                throw new Exception("Не можна залишити відгук самому собі");

            var tutorExist = await DatabaseContext.Tutors.AnyAsync(x => x.Id == r.Review.TutorId);
            if (!tutorExist)
                throw new Exception("Вчителя з таким профілем не знайдено");

            var dbReview = await DatabaseContext.Reviews
                .Include(x => x.Author)
                .Include(x => x.Tutor)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.AuthorId == r.Review.AuthorId && x.TutorId == r.Review.TutorId);

            if (dbReview == null) //Доданя нового відгука
            {
                var countLessons = DatabaseContext.Lessons.Count(l =>
                    l.TutorId == r.Review.TutorId &&
                    l.Students.Any(s => s.Id == r.Review.AuthorId));
                if (countLessons == 0)
                    throw new Exception("У вас не було жодного уроку з цим викладачем");

                dbReview = Mapper.Map<ReviewModel>(r.Review);
                await DatabaseContext.AddAsync(dbReview);
                DatabaseContext.Entry(dbReview).Reference(x => x.Author).Load();
                DatabaseContext.Entry(dbReview).Reference(x => x.Tutor).Load();
                DatabaseContext.Entry(dbReview.Tutor).Reference(x => x.User).Load();
            }
            else //Оновленя існуючого відгука
            {
                dbReview.Rating = r.Review.Rating;
                dbReview.Description = r.Review.Description;
                DatabaseContext.Update(dbReview);
            }

            //Збереження у базу даних
            await DatabaseContext.SaveChangesAsync();
            //Видача оновленого запису
            var review = Mapper.Map<Review>(dbReview);

            return review;
        }
    }
}