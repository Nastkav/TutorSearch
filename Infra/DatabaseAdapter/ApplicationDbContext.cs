using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter;

public class ApplicationDbContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // public virtual DbSet<UserModel> Users { get; set; } = null!;
    public virtual DbSet<CityModel> Cities { get; set; } = null!;
    public virtual DbSet<SubjectModel> Subjects { get; set; } = null!;
    public virtual DbSet<TutorProfile> TutorProfiles { get; set; } = null!;
    public virtual DbSet<CourseModel> Courses { get; set; } = null!;
    public virtual DbSet<LessonModel> Lessons { get; set; } = null!;
    public virtual DbSet<TaskModel> Tasks { get; set; } = null!;
    public virtual DbSet<SolutionModel> Solutions { get; set; } = null!;
    public virtual DbSet<FileModel> Files { get; set; } = null!;
    public virtual DbSet<RequestModel> Requests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        FavoriteTutor.OnModelCreating(builder);
        base.OnModelCreating(builder);
    }

    private void OnBeforeSaving()
    {
        var userId = 99999; //TODO: Get Real UserId
        var entries = ChangeTracker.Entries();
        ITrackable.BeforeSaving(entries, userId);
    }
}