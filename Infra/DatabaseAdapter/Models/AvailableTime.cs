using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class AvailableTime : ITrackable
{
    public int Id { get; set; }
    public TutorProfile Profile { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}