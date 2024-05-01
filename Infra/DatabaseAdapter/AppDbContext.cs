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

    public virtual DbSet<TutorProfileModel> TutorProfiles { get; set; } = null!;

    public virtual DbSet<AvailableTime> AvailableTimes { get; set; } = null!;
    public virtual DbSet<CourseModel> Courses { get; set; } = null!;
    public virtual DbSet<LessonModel> Lessons { get; set; } = null!;
    public virtual DbSet<TaskModel> Tasks { get; set; } = null!;
    public virtual DbSet<SolutionModel> Solutions { get; set; } = null!;
    public virtual DbSet<FileModel> Files { get; set; } = null!;
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
            entity.HasKey(x => new { UserModelId = x.UserId, TutorProfileId = x.ProfileId });
            entity.HasOne(x => x.User)
                .WithMany(x => x.FavoriteTutors)
                .HasForeignKey(x => x.UserId);
        });

        builder.Entity<UserModel>(entity =>
        {
            entity.HasOne(x => x.TutorProfile).WithOne(x => x.Owner)
                .HasForeignKey<TutorProfileModel>(x => x.Id);
            entity.Navigation(x => x.TutorProfile).IsRequired();
        });

        builder.Entity<TutorProfileModel>(entity =>
        {
            entity.HasOne(x => x.About).WithOne()
                .HasForeignKey<AboutTutorModel>(x => x.Id);
            entity.Navigation(x => x.About).IsRequired();
        });

        builder.Entity<LessonModel>()
            .HasMany(x => x.Students)
            .WithMany(x => x.Lessons)
            .UsingEntity("StudentLessons");

        builder.Entity<TutorProfileModel>()
            .HasMany(x => x.Subjects)
            .WithMany(x => x.Profiles)
            .UsingEntity("ProfileSubjects");

        builder.Entity<CourseModel>()
            .HasMany(x => x.Students)
            .WithMany(x => x.Courses)
            .UsingEntity("StudentsOnCourse");
        builder.Entity<AvailableTime>()
            .Property(x => x.StartTime)
            .HasConversion(new TimeOnlyConverter());
        builder.Entity<AvailableTime>()
            .Property(x => x.EndTime)
            .HasConversion(new TimeOnlyConverter());
        InitialSeed(builder);
        base.OnModelCreating(builder);

        #region Owner

        // builder.Entity<RequestModel>()
        //     .HasOne(x => x.Owner)
        //     .WithMany(x => x.Requests)
        //     .HasForeignKey(x => x.CreatedBy)
        //     .IsRequired();

        // builder.Entity<SolutionModel>()
        //     .HasOne(x => x.Owner)
        //     .WithMany(x => x.Solutions)
        //     .HasForeignKey(x => x.CreatedBy)
        //     .IsRequired();

        // builder.Entity<CourseReviewModel>()
        //     .HasOne(x => x.Owner)
        //     .WithMany(x => x.Reviews)
        //     .HasForeignKey(x => x.CreatedBy)
        //     .IsRequired();
        //
        // builder.Entity<FileModel>()
        //     .HasOne(x => x.Owner)
        //     .WithMany(x => x.Files)
        //     .HasForeignKey(x => x.CreatedBy)
        //     .IsRequired();

        #endregion
    }

    private void OnBeforeSaving()
    {
        var userId = 99999; //TODO: Get Real UserId
        var entries = ChangeTracker.Entries();
        ITrackable.BeforeSaving(entries, userId);
    }
}