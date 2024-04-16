using AutoMapper;
using Domain.DrivingPort.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.DrivingPort.Queries;

public class GetTutorProfileQuery : IRequest<TutorDto>
{
    public int ProfileId { get; set; }

    public class GetTutorProfileQueryHandler : BaseMediatrHandler<GetTutorProfileQuery, TutorDto>
    {
        public GetTutorProfileQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<TutorDto> Handle(GetTutorProfileQuery request,
            CancellationToken cancellationToken) =>
            GetExampleProfile(request.ProfileId);

        public static TutorDto GetExampleProfile(int id) //TODO: GetExampleProfile
        {
            List<string> result = ["Maths", "Biotechnology", "Physics", "Psychology", "English", "Chemistry"];
            var i = 0;
            var subj = result.ToDictionary(item => i++); //TODO: GetSubjects();
            switch (id)
            {
                case 1:
                    subj = subj.Where(x => x.Key < 2).ToDictionary();
                    break;
                case 2:
                    subj = subj.Where(x => x.Key > 3).ToDictionary();
                    break;
                case 3:
                    subj = subj.Where(x => x.Key == 0 || x.Key == 5).ToDictionary();
                    break;
                case 4:
                    subj = subj.Where(x => x.Key == 2 || x.Key > 3).ToDictionary();
                    break;
                default:
                    break;
            }

            // RATING
            var reviews = new List<ReviewDto>();
            reviews.Add(new ReviewDto
                { Id = 1, AuthorId = 1, TutorId = 1, Rating = 3, Comment = "Comment 1", CreatedAt = DateTime.Now });
            reviews.Add(new ReviewDto
            {
                Id = 2, AuthorId = 1, TutorId = 2, Rating = 5, Comment = "Comment 2",
                CreatedAt = DateTime.Now - TimeSpan.FromHours(1)
            });
            reviews.Add(new ReviewDto
            {
                Id = 3, AuthorId = 2, TutorId = 1, Rating = 4, Comment = "Comment 3",
                CreatedAt = DateTime.Now - TimeSpan.FromDays(1)
            });
            var count = reviews.Where(x => x.TutorId == id).Count();
            var average = 0f;
            if (count > 0)
                average = Convert.ToSingle(reviews.Where(x => x.TutorId == id).Average(x => x.Rating));
            var tutorRating = new TutorRatingDto(average, count) { ListReviews = reviews };

            var about_text = @"
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce eget dapibus urna. Sed in suscipit quam, eget blandit mauris. Nunc nec imperdiet metus, eu accumsan diam. Nulla convallis nisi feugiat mauris viverra convallis. In varius quam nibh, et consequat dui sagittis eget. Quisque ac imperdiet lectus. Ut sed gravida libero. Integer ut metus elementum, sagittis massa eu, auctor lacus. In interdum ligula ligula, sit amet tempor est condimentum fermentum. In blandit purus erat, ac aliquam odio malesuada at. Fusce vel suscipit tellus. Pellentesque dictum tincidunt mauris vitae tincidunt.

Nam posuere sollicitudin tempus. Etiam vitae elit facilisis, euismod magna a, iaculis nibh. Donec lorem orci, dapibus malesuada finibus sed, interdum id ante. Nunc dapibus, sem sed volutpat faucibus, dui tortor consequat lorem, bibendum ullamcorper tellus diam at libero. Nunc tempus mauris et eros fringilla, id maximus ante convallis. Integer tempor, elit eget mollis imperdiet, est nulla hendrerit lacus, id tincidunt mauris lacus a justo. Nam vel enim erat. Curabitur quam dolor, gravida ac diam eu, sagittis facilisis mauris. Phasellus tincidunt mauris sed orci rhoncus, in pretium turpis lobortis. Vestibulum id lacus vel neque ultricies ornare. Vivamus lorem sem, pellentesque in imperdiet aliquet, porta non velit. Fusce vel erat at odio semper varius eu et enim. Fusce feugiat non felis a feugiat.

Nullam consequat dolor ut enim fermentum, id ultricies sapien condimentum. Duis elementum ex vitae eros ultrices porttitor. Curabitur turpis risus, elementum quis sapien id, molestie accumsan turpis. Sed vel tincidunt tortor, quis lobortis nibh. Sed in felis non mi faucibus sodales eget eu massa. Sed non interdum dolor. Duis euismod vehicula augue.
";
            var tutor = new TutorDto
            {
                Id = id,
                Enabled = true,
                ImgPath = "/img/bd364400-6dc2-4e1b-9a86-919e981710e1.jpg",
                Address = "Богунський, Центр, вул. Михайлівська 20",
                City = "Харків",
                Experience = "2 роки",
                Descriptions = "Descriptions Descriptions Descriptions Descriptions Descriptions Descriptions",
                About = about_text,
                HourRate = 320,
                OnlineAccess = false,
                AtHomeAccess = true,
                OffsiteAccess = true,
                Subjects = subj,
                ReviewCount = tutorRating.Count,
                RatingValue = tutorRating.Average
            };
            //TODO: GetTutorProfileQueryHandler
            return tutor;
        }
    }
}