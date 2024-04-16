using Domain.DrivingPort.Models;
using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class GetTutorsQuery : IRequest<List<TutorDto>>
{
    public decimal? HourRateFrom { get; set; }
    public decimal? HourRateTo { get; set; }
    public string? City { get; set; }
    public string? Subject { get; set; }
    public bool? OnlineAccess { get; set; }
    public bool? AtHomeAccess { get; set; }
    public bool? OffsiteAccess { get; set; }
    public string SearchQuery { get; set; } = ""; // Поле вводу з кнопкою пошуку

    public class GetTutorsQueryHandler : BaseMediatrHandler<GetTutorsQuery, List<TutorDto>>
    {
        public GetTutorsQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<List<TutorDto>> Handle(GetTutorsQuery request,
            CancellationToken cancellationToken)
        {
            //TODO: GetTutorsQueryHandler
            List<TutorDto> tutors = new();
            for (var i = 1; i < 4; i++)
                tutors.Add(GetTutorProfileQuery.GetTutorProfileQueryHandler.GetExampleProfile(i));
            return tutors;
        }
    }
}