using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infra.DatabaseAdapter.Helpers;

public interface ITrackable
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }

    static void BeforeSaving(IEnumerable<EntityEntry> entries)
    {
        var now = DateTime.UtcNow;
        foreach (var entry in entries)
            if (entry.Entity is ITrackable trackable)
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedAt = now;
                        entry.Property("CreatedAt").IsModified = false;
                        break;
                    case EntityState.Added:
                        trackable.CreatedAt = now;
                        trackable.UpdatedAt = now;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
    }
}