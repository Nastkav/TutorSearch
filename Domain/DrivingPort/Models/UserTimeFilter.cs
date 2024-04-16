using Domain.Port.Driving;

namespace Domain.DrivingPort.Models;

public class UserTimeFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<TimeType> TimeTypes { get; set; } = [];
}