using AutoMapper;
using Domain.Models;
using Infra.DatabaseAdapter.Models;
using Web.Models.TutorProfile;

namespace Web.Helpers;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        // CreateMap<TutorProfileModel, TutorCardVm>()
        //     .ForMember(d => d.Subjects, o => o.MapFrom(x => x.Subjects.ToList()))
        //     .ForMember(d => d.City, o => o.Ignore());
        // ;
        //
        //
        // //TutorDto -> TutorCardViewModel
        // CreateMap<TutorDto, TutorCardVm>()
        //     .ForMember(d => d.Subjects, o => o.MapFrom(x => x.Subjects.Values.ToList()))
        //     ;
    }
}