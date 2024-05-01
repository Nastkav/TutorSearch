using AutoMapper;
using Domain.Commands;
using Domain.DrivingPort.Models;
using Domain.Models;
using Infra.DatabaseAdapter.Models;

namespace Domain.Helpers;

public class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        // CreateMap<TutorDto, TutorProfileModel>()
        //     .ForMember(d => d.About, o => o.Ignore())
        //     .ForMember(d => d.City, o => o.Ignore())


        CreateMap<TutorEditDto, TutorProfileModel>()
            .ForMember(d => d.About, o => o.Ignore())
            .ForMember(d => d.City, o => o.Ignore());


        CreateMap<CreateRequestCommand, RequestModel>()
            .ForMember(d => d.TutorId, o => o.MapFrom(x => x.TutorProfileId))
            .ForMember(d => d.Subject, o => o.Ignore());
        CreateMap<UpdateRequestCommand, RequestModel>()
            .ForMember(d => d.TutorId, o => o.MapFrom(x => x.TutorProfileId))
            .ForMember(d => d.Subject, o => o.Ignore())
            .ForAllMembers(opts => { opts.Condition((_, _, srcMember) => srcMember != null); });

        CreateMap<RequestModel, LessonRequestDto>();

        CreateMap<UpdateRequestCommand, LessonModel>()
            .ForMember(d => d.Subject, o => o.Ignore());
        CreateMap<RequestModel, LessonModel>()
            .ForMember(d => d.TutorProfileId, o => o.MapFrom(x => x.TutorId));
        CreateMap<LessonModel, LessonDetailsDto>()
            .ForMember(d => d.Title, o => o.MapFrom(x => $"{x.Subject.Name} - {x.Course.Title}"))
            .ForMember(d => d.Subject, o => o.MapFrom(x => x.Subject.Name))
            .ForMember(d => d.CourseId, o => o.MapFrom(x => x.Course.Id))
            .ForMember(d => d.CourseName, o => o.MapFrom(x => x.Course.Title));


        // TutorProfileModel <-> TutorDto
        CreateMap<TutorDto, TutorProfileModel>()
            .ForMember(d => d.About, o => o.Ignore())
            .ForMember(d => d.City, o => o.Ignore())
            ;
        CreateMap<TutorProfileModel, TutorDto>()
            .ForMember(d => d.About, o => o.Ignore())
            .ForMember(d => d.City, o => o.MapFrom(x => x.City.FullName()))
            ;
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