using AutoMapper;
using Domain.Models;
using Infra.DatabaseAdapter.Models;
using Web.Models.TutorProfile;

namespace Web.Helpers;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<TutorProfileModel, TutorCardViewModel>()
            .ForMember(d => d.Subjects, o => o.MapFrom(x => x.Subjects.ToList()))
            .ForMember(d => d.City, o => o.Ignore());
        ;


        //TutorDto -> TutorCardViewModel
        CreateMap<TutorDto, TutorCardViewModel>()
            .ForMember(d => d.Subjects, o => o.MapFrom(x => x.Subjects.Values.ToList()))
            ;


        CreateMap<TutorDto, OLD_DetailsViewModel>()
            .ForMember(d => d.Subjects,
                o => o.Ignore())
            .AfterMap((dto, vm, context) => vm.TutorCard = context.Mapper.Map<TutorCardViewModel>(dto));

        CreateMap<OLD_DetailsViewModel, TutorEditDto>();
        CreateMap<TutorCardViewModel, TutorEditDto>();
    }
}