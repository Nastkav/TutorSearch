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
            .ReverseMap();

        CreateMap<TutorProfileModel, Tutor>()
            .ForMember(d => d.About, o => o.MapFrom(x => x.About.Content))
            .ForMember(d => d.Subjects, o => o.Ignore())
            .AfterMap((s, d) => d.ImgPath = s.ImgPath != string.Empty ? s.ImgPath : "/img/example_face.jpg");
        CreateMap<Tutor, TutorProfileModel>()
            .ForPath(d => d.About.Content, o => o.MapFrom(x => x.About))
            .ForPath(d => d.About.Id, o => o.MapFrom(x => x.Id))
            .ForMember(d => d.Subjects, o => o.Ignore())
            .AfterMap((s, d) => d.ImgPath = s.ImgPath != string.Empty ? s.ImgPath : "/img/example_face.jpg");

        //Requests
        CreateMap<LessonRequest, RequestModel>();
        CreateMap<RequestModel, LessonRequest>();
        CreateMap<CreateRequestCommand, RequestModel>();
        CreateMap<UpdateRequestCommand, RequestModel>()
            .ForMember(d => d.Subject, o => o.Ignore())
            .ForAllMembers(opts => { opts.Condition((_, _, srcMember) => srcMember != null); });

        // CreateMap<CreateRequestCommand, RequestModel>()
        //     .ForMember(d => d.TutorId, o => o.MapFrom(x => x.TutorProfileId))
        //     .ForMember(d => d.Subject, o => o.Ignore());
        // CreateMap<UpdateRequestCommand, RequestModel>()
        //     .ForMember(d => d.TutorId, o => o.MapFrom(x => x.TutorProfileId))
        //     .ForMember(d => d.Subject, o => o.Ignore())
        //     .ForAllMembers(opts => { opts.Condition((_, _, srcMember) => srcMember != null); });
        //
        // CreateMap<RequestModel, LessonRequestDto>();
        //
        // CreateMap<UpdateRequestCommand, LessonModel>()
        //     .ForMember(d => d.Subject, o => o.Ignore());
        // CreateMap<RequestModel, LessonModel>()
        //     .ForMember(d => d.TutorProfileId, o => o.MapFrom(x => x.TutorId));
        // CreateMap<LessonModel, LessonDetailsDto>()
        //     .ForMember(d => d.Title, o => o.MapFrom(x => $"{x.Subject.Name} - {x.Course.Title}"))
        //     .ForMember(d => d.Subject, o => o.MapFrom(x => x.Subject.Name))
        //     .ForMember(d => d.CourseId, o => o.MapFrom(x => x.Course.Id))
        //     .ForMember(d => d.CourseName, o => o.MapFrom(x => x.Course.Title));
        //
        //
        // // TutorProfileModel <-> TutorDto
        // CreateMap<TutorDto, TutorProfileModel>()
        //     .ForMember(d => d.About, o => o.Ignore())
        //     .ForMember(d => d.City, o => o.Ignore())
        //     ;
        // CreateMap<TutorProfileModel, TutorDto>()
        //     .ForMember(d => d.About, o => o.Ignore())
        //     .ForMember(d => d.City, o => o.MapFrom(x => x.City.FullName()))
        //     ;
    }

    // .ForMember(d => d.Subjects,
    //     o => o.MapFrom(
    //         x => x.Subjects.Values.ToList()))
    // .ForMember(d => d.ReviewString,
    //     o => o.MapFrom(
    //         x => $"{x.RatingValue} ({x.ReviewCount} reviews)"));
    // CreateMap<TutorDto, DetailsViewModel>()
    //     .ForMember(d => d.Subjects,
    //         o => o.Ignore())
    //     .AfterMap((dto, vm, context) => vm.TutorCard = context.Mapper.Map<TutorCardViewModel>(dto));
}