using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infra.DatabaseAdapter.Models;

public class UserModel : IdentityUser<int>
{
    [MaxLength(100)] public string? Name { get; set; }

    [MaxLength(100)] public string? Surname { get; set; }
    [MaxLength(100)] public string? Patronymic { get; set; }
    public DateTime? BirthDate { get; set; }
    public CityModel? City { get; set; }
    public virtual List<FavoriteTutor> FavoriteTutors { get; set; } = [];
    public virtual List<LessonModel> Lessons { get; set; } = [];
}