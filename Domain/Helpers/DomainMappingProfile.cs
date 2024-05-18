using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Infra.DatabaseAdapter.Models;

namespace Domain.Helpers;

public class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        //User & Tutor Profiles
        CreateMap<User, UserModel>();

        CreateMap<UserModel, User>()
            .ForMember(d => d.FullName, o => o.MapFrom(x => x.FullName()));

        CreateMap<TutorModel, Tutor>()
            .ForMember(d => d.About, o => o.MapFrom(x => x.About.Content))
            .ForMember(d => d.SubjectIds, o => o.MapFrom(x => x.Subjects.Select(o => o.Id).ToList()));

        CreateMap<Tutor, TutorModel>()
            .ForPath(d => d.About.Content, o => o.MapFrom(x => x.About))
            .ForPath(d => d.About.Id, o => o.MapFrom(x => x.Id))
            .ForMember(d => d.Subjects, o => o.Ignore());

        //Files
        CreateMap<UserFileModel, UserFile>()
            .ForMember(d => d.OwnerName, o => o.MapFrom(x => x.Owner.Name));
        CreateMap<UserFile, UserFileModel>(); //TODO check string-> Guid 


        //Requests
        CreateMap<LessonRequest, RequestModel>();
        CreateMap<RequestModel, LessonRequest>();
        CreateMap<CreateRequestCommand, RequestModel>();

        CreateMap<UpdateRequestCommand, RequestModel>()
            .ForMember(d => d.Subject, o => o.Ignore())
            .ForAllMembers(opts => { opts.Condition((_, _, srcMember) => srcMember != null); });

        CreateMap<RequestModel, LessonModel>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.RequestId, o => o.MapFrom(x => x.Id))
            .ForMember(d => d.Comment, o => o.MapFrom(x => x.TutorComment));

        //Lessons
        CreateMap<LessonModel, Lesson>()
            .ForMember(d => d.StudentsIds, o => o.MapFrom(x => x.Students.Select(s => s.Id)))
            .ForMember(d => d.StudentNames, o => o.MapFrom(x =>
                string.Join(", ", x.Students.Select(s => s.FullName()))))
            .ForMember(d => d.TutorName, o => o.MapFrom(x => x.Tutor.User.FullName()));

        CreateMap<Lesson, LessonModel>()
            .ForMember(d => d.Students, o => o.Ignore())
            .ForMember(d => d.Tutor, o => o.Ignore());

        //Assignments
        CreateMap<Assignment, AssignmentModel>()
            .ForMember(d => d.Deadline, o => o.MapFrom(x => x.Deadline.ToDateTime(TimeOnly.MinValue)))
            .ForMember(d => d.Files, opt => opt.MapFrom(s => s.FileNames));


        CreateMap<AssignmentModel, Assignment>()
            .ForMember(d => d.TutorName, o => o.MapFrom(x => x.Tutor.User.FullName()))
            .ForMember(d => d.SubjectName, o => o.MapFrom(x => x.Subject.Name))
            .ForMember(d => d.Deadline, o => o.MapFrom(x => DateOnly.FromDateTime(x.Deadline)))
            .ForMember(d => d.FileNames, opt => opt.MapFrom(s => s.Files))
            .ForMember(d => d.StudentsIds, o => o.MapFrom(x => x.Solutions.Select(s => s.StudentId)))
            .ForMember(d => d.StudentNames, o => o.MapFrom(x =>
                string.Join(", ", x.Solutions.Select(s => s.Student.FullName()))));

        //Solutions
        CreateMap<Solution, SolutionModel>()
            .ForMember(d => d.Files, opt => opt.MapFrom(s => s.SolutionFiles));

        CreateMap<SolutionModel, Solution>()
            .ForMember(d => d.StudentName, o => o.MapFrom(x => x.Student.FullName()))
            .ForMember(d => d.SolutionFiles, opt => opt.MapFrom(s => s.Files))
            .ForMember(d => d.TutorId, opt => opt.MapFrom(s => s.Assignment.Tutor.Id))
            .ForMember(d => d.TutorName, o => o.MapFrom(x => x.Assignment.Tutor.User.FullName()))
            .ForMember(d => d.AssignmentTitle, o => o.MapFrom(x => x.Assignment.Title))
            .ForMember(d => d.Description, o => o.MapFrom(x => x.Assignment.Description))
            .ForMember(d => d.SubjectName, o => o.MapFrom(x => x.Assignment.Subject.Name))
            .ForMember(d => d.AssignmentFiles, opt => opt.MapFrom(s => s.Assignment.Files));


        //Review and Feedbacks
        CreateMap<Review, ReviewModel>();
        CreateMap<ReviewModel, Review>()
            .ForMember(d => d.TutorName, o => o.MapFrom(x => x.Tutor.User.FullName()))
            .ForMember(d => d.AuthorName, o => o.MapFrom(x => x.Author.FullName()));
    }
}