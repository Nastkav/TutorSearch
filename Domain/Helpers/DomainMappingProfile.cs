using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Infra.DatabaseAdapter.Models;

namespace Domain.Helpers;

public class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        //User & Tutor Profiles
        CreateMap<UserModel, User>()
            .AfterMap((s, d) => d.Avatar = s.Avatar != string.Empty ? s.Avatar : "/img/example_face.jpg");
        CreateMap<User, UserModel>()
            .AfterMap((s, d) => d.Avatar = s.Avatar != string.Empty ? s.Avatar : "/img/example_face.jpg");

        CreateMap<TutorModel, Tutor>()
            .ForMember(d => d.About, o => o.MapFrom(x => x.About.Content))
            .ForMember(d => d.SubjectIds, o => o.MapFrom(x => x.Subjects.Select(o => o.Id).ToList()));
        CreateMap<Tutor, TutorModel>()
            .ForPath(d => d.About.Content, o => o.MapFrom(x => x.About))
            .ForPath(d => d.About.Id, o => o.MapFrom(x => x.Id))
            .ForMember(d => d.Subjects, o => o.Ignore());


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
            .ForMember(d => d.SubjectName, o => o.MapFrom(x => x.Subject.Name))
            .ForMember(d => d.Students, o => o.Ignore())
            .ForMember(d => d.TutorName, o => o.MapFrom(x => x.Tutor.User.FullName()));
    }
}