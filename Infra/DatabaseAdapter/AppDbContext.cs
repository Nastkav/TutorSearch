using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;

namespace Infra.DatabaseAdapter;

public class AppDbContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
{
    private bool _withSeed;

    public AppDbContext(DbContextOptions<AppDbContext> options, bool withSeed = false) : base(options) =>
        _withSeed = withSeed;

    public DbSet<CityModel> Cities { get; set; } = null!;
    public DbSet<SubjectModel> Subjects { get; set; } = null!;
    public DbSet<TutorModel> Tutors { get; set; } = null!;
    public DbSet<AvailableTimeModel> AvailableTimes { get; set; } = null!;
    public DbSet<LessonModel> Lessons { get; set; } = null!;
    public DbSet<AssignmentModel> Assignments { get; set; } = null!;
    public DbSet<SolutionModel> Solutions { get; set; } = null!;
    public DbSet<UserFileModel> Files { get; set; } = null!;
    public DbSet<RequestModel> Requests { get; set; } = null!;
    public DbSet<ReviewModel> Reviews { get; set; } = null!;
    // public DbSet<UserModel> Users { get; set; } = null!; // Inctude in IdentityDbContext


    private void InitialSeed(ModelBuilder builder)
    {
        builder.Entity<IdentityRole<int>>().HasData(DataSeed.Roles);
        builder.Entity<SubjectModel>().HasData(DataSeed.Subjects);
        builder.Entity<CityModel>().HasData(DataSeed.Cities);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserModel>(entity =>
        {
            entity.HasOne(x => x.Tutor)
                .WithOne(x => x.User)
                .HasForeignKey<TutorModel>(x => x.Id);
            entity.Navigation(x => x.Tutor)
                .IsRequired();
            entity.HasMany(x => x.FavoriteTutors)
                .WithMany(x => x.InFavorite)
                .UsingEntity("Favorites");
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
        if (_withSeed)
            InitialSeed(builder);
        base.OnModelCreating(builder);
    }


    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .ToList();
        ITrackable.BeforeSaving(entries);
        UserModel.BeforeSaving(entries);

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .ToList();
        ITrackable.BeforeSaving(entries);
        UserModel.BeforeSaving(entries);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}