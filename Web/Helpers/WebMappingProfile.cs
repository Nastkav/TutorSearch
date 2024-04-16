using AutoMapper;
using Domain.DrivingPort.Models;
using Web.Models.TutorProfile;

namespace Web.Helpers;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<TutorDto, TutorCardViewModel>()
            .ForMember(d => d.Subjects,
                o => o.MapFrom(
                    x => x.Subjects.Values.ToList()))
            .ForMember(d => d.ReviewString,
                o => o.MapFrom(
                    x => $"{x.RatingValue} ({x.ReviewCount} reviews)"));

        CreateMap<TutorDto, DetailsViewModel>()
            .ForMember(d => d.Subjects,
                o => o.Ignore())
            .AfterMap((dto, vm, context) => vm.TutorCard = context.Mapper.Map<TutorCardViewModel>(dto));
    }
}