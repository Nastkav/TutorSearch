using Infra.Ports;
using MediatR;

namespace Domain.DrivingPort.Queries;

public class GetAllCitiesQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllCitiesQueryHandler : BaseMediatrHandler<GetAllCitiesQuery, Dictionary<int, string>>
    {
        public GetAllCitiesQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }


        public override async Task<Dictionary<int, string>> Handle(GetAllCitiesQuery request,
            CancellationToken cancellationToken) =>
            new() //TODO: GetAllCitiesQueryHandler
            {
                { 1, "Житомир" }, { 2, "Одеса" }, { 3, "Харків" }, { 4, "Київ" }
            };
    }
}