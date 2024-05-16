using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;

namespace Infra.DatabaseAdapter;

public class AppDbContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // public virtual DbSet<UserModel> Users { get; set; } = null!;
    public virtual DbSet<CityModel> Cities { get; set; } = null!;
    public virtual DbSet<SubjectModel> Subjects { get; set; } = null!;
    public virtual DbSet<TutorModel> Tutor { get; set; } = null!;
    public virtual DbSet<AvailableTimeModel> AvailableTimes { get; set; } = null!;
    public virtual DbSet<LessonModel> Lessons { get; set; } = null!;
    public virtual DbSet<AssignmentModel> Assignments { get; set; } = null!;
    public virtual DbSet<SolutionModel> Solutions { get; set; } = null!;
    public virtual DbSet<UserFileModel> Files { get; set; } = null!;
    public virtual DbSet<RequestModel> Requests { get; set; } = null!;
    public virtual DbSet<FavoriteTutorModel> FavoriteTutors { get; set; } = null!;

    private void InitialSeed(ModelBuilder builder)
    {
        builder.Entity<SubjectModel>().HasData(DataSeed.Subjects);
        builder.Entity<CityModel>().HasData(DataSeed.Cities);
        builder.Entity<IdentityRole<int>>().HasData(DataSeed.Roles);
        builder.Entity<UserModel>().HasData(DataSeed.Users);
        builder.Entity<IdentityUserRole<int>>().HasData(DataSeed.UserRoles);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FavoriteTutorModel>(entity =>
        {
            entity.HasKey(x => new { UserModelId = x.UserId, TutorId = x.ProfileId });
            entity.HasOne(x => x.User)
                .WithMany(x => x.FavoriteTutors)
                .HasForeignKey(x => x.UserId);
        });

        builder.Entity<UserModel>(entity =>
        {
            entity.HasOne(x => x.Tutor).WithOne(x => x.User)
                .HasForeignKey<TutorModel>(x => x.Id);
            entity.Navigation(x => x.Tutor).IsRequired();
        });

        builder.Entity<TutorModel>(entity =>
        {
            entity.HasOne(x => x.About).WithOne()
                .HasForeignKey<AboutTutorModel>(x => x.Id);
            entity.Navigation(x => x.About).IsRequired();
        });

        builder.Entity<LessonModel>()
            .HasMany(x => x.Students)
            .WithMany(x => x.Lessons)
            .UsingEntity("StudentLessons");

        builder.Entity<TutorModel>()
            .HasMany(x => x.Subjects)
            .WithMany(x => x.Profiles)
            .UsingEntity("ProfileSubjects");

        builder.Entity<AssignmentModel>()
            .HasMany(x => x.Files)
            .WithMany(x => x.Assignments)
            .UsingEntity("AssignmentFiles");

        builder.Entity<SolutionModel>()
            .HasMany(x => x.Files)
            .WithMany(x => x.Solutions)
            .UsingEntity("SolutiontFiles");

        builder.Entity<AvailableTimeModel>()
            .Property(x => x.StartTime)
            .HasConversion(new TimeOnlyConverter());
        builder.Entity<AvailableTimeModel>()
            .Property(x => x.EndTime)
            .HasConversion(new TimeOnlyConverter());

        //Seed
        InitialSeed(builder);
        base.OnModelCreating(builder);
    }


    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ITrackable.BeforeSaving(ChangeTracker.Entries());
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        ITrackable.BeforeSaving(ChangeTracker.Entries());
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}