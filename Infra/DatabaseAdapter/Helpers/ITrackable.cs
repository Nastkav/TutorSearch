using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infra.DatabaseAdapter.Helpers;

public interface ITrackable
{
    DateTime CreatedAt { get; set; }
    int CreatedId { get; set; }
    DateTime? UpdatedAt { get; set; }
    int? UpdatedId { get; set; }

    static void BeforeSaving(IEnumerable<EntityEntry> entries, int userId)
    {
        var now = DateTime.UtcNow;
        foreach (var entry in entries)
            if (entry.Entity is ITrackable trackable)
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedAt = now;
                        // trackable.UpdatedBy = userId;
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        break;
                    case EntityState.Added:
                        trackable.CreatedAt = now;
                        // trackable.CreatedBy = userId;
                        trackable.UpdatedAt = now;
                        trackable.UpdatedId = userId;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
    }
}