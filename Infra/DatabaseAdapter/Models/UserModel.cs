using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infra.DatabaseAdapter.Models;

public class UserModel : IdentityUser<int>
{
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    [MaxLength(50)] public string Surname { get; set; } = string.Empty;
    [MaxLength(50)] public string Patronymic { get; set; } = string.Empty;
    public bool ProfileEnabled { get; set; }
    public TutorModel Tutor { get; set; } = null!;

    public int? CityId { get; set; }
    public CityModel? City { get; set; }
    public DateTime? BirthDate { get; set; }

    public virtual List<TutorModel> FavoriteTutors { get; set; } = [];
    public virtual List<LessonModel> Lessons { get; set; } = [];
    public virtual List<RequestModel> Requests { get; set; } = [];
    public virtual List<SolutionModel> Solutions { get; set; } = [];
    public virtual List<ReviewModel> Reviews { get; set; } = [];

    public virtual List<UserFileModel> Files { get; set; } = [];

    public string FullName() => Name.Length > 2 ? $"{Name} {Patronymic} {Surname}" : Email ?? "";

    public string NormalizeName { get; set; } = string.Empty;

    public static void BeforeSaving(IEnumerable<EntityEntry> entries)
    {
        foreach (var entry in entries)
            if (entry.Entity is UserModel u)
                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    if (u.Name.Length > 2)
                        u.NormalizeName = string.Join(' ', u.Name, u.Patronymic, u.Surname);
                    else
                        u.NormalizeName = u.Email ?? "";

                    u.NormalizeName = u.NormalizeName.ToUpperInvariant();
                }
    }
}