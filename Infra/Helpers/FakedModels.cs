using Bogus;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;

namespace Infra.Helpers;

public class FakedModels
{
    public UserModel GenerateUserModel(bool profileEnable = false) => GenerateUserModel(1, profileEnable)[0];

    public List<UserModel> GenerateUserModel(int count, bool profileEnable = false) =>
        new Faker<UserModel>("uk")
            .RuleFor(x => x.Name, f => f.Name.FirstName(f.Person.Gender))
            .RuleFor(x => x.Surname, f => f.Name.LastName(f.Person.Gender))
            .RuleFor(x => x.Patronymic, f => f.Name.FirstName(f.Person.Gender))
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.ProfileEnabled, profileEnable)
            .RuleFor(x => x.City, f => new CityModel() { Name = f.Address.City(), Region = f.Address.Direction() })
            .RuleFor(x => x.BirthDate, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date)
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .FinishWith((f, u) => { Console.WriteLine("User Created! Id={0}", u.Name); })
            .Generate(count);


    public TutorModel GenerateTutorModel() => GenerateTutorModel(1)[0];

    public List<TutorModel> GenerateTutorModel(int count) =>
        new Faker<TutorModel>("uk")
            .RuleFor(x => x.User, f => GenerateUserModel(true))
            .RuleFor(x => x.About, f => new AboutTutorModel() { Content = f.Lorem.Paragraphs(20) })
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.OnlineAccess, f => f.Random.Bool())
            .RuleFor(x => x.TutorHomeAccess, f => f.Random.Bool())
            .RuleFor(x => x.StudentHomeAccess, f => f.Random.Bool())
            .RuleFor(x => x.Descriptions, f => f.Lorem.Sentence(10))
            .RuleFor(x => x.HourRate, f => f.Random.Int(0, 9999))
            .Generate(count);


    public LessonModel GenerateLessonModel() => GenerateLessonModel(1)[0];

    public List<LessonModel> GenerateLessonModel(int count) =>
        new Faker<LessonModel>("uk")
            .RuleFor(x => x.Tutor, f => GenerateTutorModel())
            .RuleFor(x => x.From, f => f.Date.Past(1))
            .RuleFor(x => x.To, (f, x) => x.From.AddHours(f.Random.Int(1, 3)))
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Comment, f => f.Lorem.Paragraphs(1))
            .RuleFor(x => x.SubjectId, f => f.Random.Int(0, 10))
            .RuleFor(x => x.Subject, f => GenerateSubjectModel())
            .Generate(count);

    public CityModel GenerateCityModel() => GenerateCityModel(1)[0];

    public List<CityModel> GenerateCityModel(int count) =>
        new Faker<CityModel>("uk")
            .RuleFor(x => x.Name, f => f.Address.City())
            .RuleFor(x => x.Region, f => f.Address.State())
            .Generate(count);


    public SubjectModel GenerateSubjectModel() => GenerateSubjectModel(1)[0];

    public List<SubjectModel> GenerateSubjectModel(int count) =>
        new Faker<SubjectModel>("uk")
            .RuleFor(x => x.Name, f => new SubjectSet().Generate())
            .Generate(count);


    public AssignmentModel GenerateAssignmentModel() => GenerateAssignmentModel(1)[0];

    public List<AssignmentModel> GenerateAssignmentModel(int count) =>
        new Faker<AssignmentModel>("uk")
            .RuleFor(x => x.Tutor, f => GenerateTutorModel())
            .RuleFor(x => x.Subject, f => GenerateSubjectModel())
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Description, f => f.Lorem.Paragraphs(3))
            .RuleFor(x => x.Deadline, f => f.Date.Future(1))
            .Generate(count);

    public SolutionModel GenerateSolutionModel() => GenerateSolutionModel(1)[0];

    public List<SolutionModel> GenerateSolutionModel(int count) =>
        new Faker<SolutionModel>("uk")
            .RuleFor(x => x.Assignment, f => GenerateAssignmentModel())
            .RuleFor(x => x.Student, f => GenerateUserModel())
            .RuleFor(x => x.Status, f => f.Random.Enum<SolutionStatus>())
            .RuleFor(x => x.Answer, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.TutorComment, f => f.Lorem.Paragraphs(2))
            .Generate(count);


    public ReviewModel GenerateReviewModel() => GenerateReviewModel(1)[0];


    public List<ReviewModel> GenerateReviewModel(int count) =>
        new Faker<ReviewModel>("uk")
            .RuleFor(x => x.Tutor, f => GenerateTutorModel())
            .RuleFor(x => x.Rating, f => f.Random.Int(0, 10))
            .RuleFor(x => x.Description, f => f.Lorem.Paragraphs(3))
            .RuleFor(x => x.Author, f => GenerateUserModel())
            .RuleFor(x => x.CreatedAt, f => f.Date.Past(3))
            .RuleFor(x => x.UpdatedAt, f => f.Date.Past(3))
            .Generate(count);


    public RequestModel GenerateRequestModel() => GenerateRequestModel(1)[0];

    public List<RequestModel> GenerateRequestModel(int count) =>
        new Faker<RequestModel>("uk")
            .RuleFor(x => x.Tutor, f => GenerateTutorModel())
            .RuleFor(x => x.Comment, f => f.Lorem.Paragraphs(1))
            .RuleFor(x => x.TutorComment, f => f.Lorem.Paragraphs(1))
            .RuleFor(x => x.Subject, f => GenerateSubjectModel())
            .RuleFor(x => x.Created, f => GenerateUserModel())
            .RuleFor(x => x.Status, f => f.Random.Enum<LessonRequestStatus>())
            .RuleFor(x => x.From, f => f.Date.Past(1))
            .RuleFor(x => x.To, (f, x) => x.From.AddHours(f.Random.Int(1, 3)))
            .Generate(count);
}