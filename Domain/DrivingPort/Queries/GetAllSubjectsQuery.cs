using Domain.DrivingPort.Models;
using Domain.Port.Driving;
using Infra.DatabaseAdapter;
using Infra.Ports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.DrivingPort.Queries;

public class GetAllSubjectsQuery : IRequest<Dictionary<int, string>>
{
    public class GetAllSubjectsQueryHandler : BaseMediatrHandler<GetAllSubjectsQuery, Dictionary<int, string>>
    {
        public GetAllSubjectsQueryHandler(IEventRepository eventRepo, IUserRepository userRepo)
            : base(eventRepo, userRepo) { }

        public override async Task<Dictionary<int, string>> Handle(GetAllSubjectsQuery request,
            CancellationToken cancellationToken)
        {
            //TODO: GetAllSubjectsQueryHandler

            List<string> result = ["Maths", "Biotechnology", "Physics", "Psychology", "English", "Chemistry"];
            var i = 0;
            return result.ToDictionary(item => i++);
        }
    }
}