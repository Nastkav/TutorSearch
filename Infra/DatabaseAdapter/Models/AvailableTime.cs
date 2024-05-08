using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class AvailableTimeModel : ITrackable
{
    public int Id { get; set; }
    public int DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int ProfileId { get; set; }
    public TutorModel Profile { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedId { get; set; }
}