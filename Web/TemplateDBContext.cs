using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Commands;

namespace Web;

public class TemplateDbContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
{
    public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
    {
    }


    // public virtual DbSet<UserModel> Users { get; set; } = null!;
    public virtual DbSet<CityModel> Cities { get; set; } = null!;
    public virtual DbSet<SubjectModel> Subjects { get; set; } = null!;

    public virtual DbSet<TutorModel> Tutors { get; set; } = null!;

    public virtual DbSet<AvailableTimeModel> AvailableTimes { get; set; } = null!;
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
        var entityFavoriteTutor = builder.Entity<FavoriteTutorModel>(entity =>
        {
            entity.HasKey(x => new { UserModelId = x.UserId, TutorId = x.ProfileId });
            entity.HasOne(x => x.User)
                .WithMany(x => x.FavoriteTutors)
                .HasForeignKey(x => x.UserId);
        });

        builder.Entity<UserModel>(entity =>
        {
            entity.HasOne(o => o.Tutor).WithOne()
                .HasForeignKey<TutorModel>(o => o.Id);
            entity.Navigation(o => o.Tutor).IsRequired();
        });

        builder.Entity<UserModel>(entity =>
        {
            entity.HasOne(o => o.Tutor).WithOne()
                .HasForeignKey<TutorModel>(o => o.Id);
            entity.Navigation(o => o.Tutor).IsRequired();
        });

        builder.Entity<TutorModel>(entity =>
        {
            entity.HasOne(o => o.About).WithOne()
                .HasForeignKey<AboutTutorModel>(o => o.Id);
            entity.Navigation(o => o.About).IsRequired();
        });

        builder.Entity<LessonModel>()
            .HasMany(e => e.Students)
            .WithMany(e => e.Lessons)
            .UsingEntity("StudentLessons");

        builder.Entity<TutorModel>()
            .HasMany(e => e.Subjects)
            .WithMany(e => e.Profiles)
            .UsingEntity("ProfileSubjects");

        builder.Entity<AvailableTimeModel>()
            .Property(p => p.StartTime)
            .HasConversion(new TimeOnlyConverter());
        builder.Entity<AvailableTimeModel>()
            .Property(p => p.EndTime)
            .HasConversion(new TimeOnlyConverter());
        // InitialSeed(builder);
        base.OnModelCreating(builder);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        ITrackable.BeforeSaving(entries);
    }

    public DbSet<UpdateRequestCommand> UpdateRequestCommand { get; set; } = default!;
}