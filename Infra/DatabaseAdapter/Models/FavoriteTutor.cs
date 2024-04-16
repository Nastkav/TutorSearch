using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter.Models;

public class FavoriteTutor
{
    public int UserModelId { get; set; }
    public UserModel UserModel { get; set; } = null!;
    public int TutorProfileId { get; set; }
    public TutorProfile TutorProfile { get; set; } = null!;

    public static void OnModelCreating(ModelBuilder builder)
    {
        var e = builder.Entity<FavoriteTutor>();
        e.HasKey(x => new { x.UserModelId, x.TutorProfileId });
        e.HasOne(x => x.UserModel)
            .WithMany(x => x.FavoriteTutors)
            .HasForeignKey(x => x.UserModelId);
    }
}