using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infra.DatabaseAdapter.Models;

public class UserModel : IdentityUser<int>
{
    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(100)] public string Surname { get; set; } = string.Empty;
    [MaxLength(100)] public string Patronymic { get; set; } = string.Empty;

    public bool TutorProfileEnabled { get; set; }

    public int? CityId { get; set; }
    public CityModel? City { get; set; }
    public DateTime? BirthDate { get; set; }
    public virtual List<FavoriteTutorModel> FavoriteTutors { get; set; } = [];
    public virtual List<LessonModel> Lessons { get; set; } = [];
    public virtual List<CourseModel> Courses { get; set; } = [];
    public virtual List<RequestModel> Requests { get; set; } = [];
    public virtual List<SolutionModel> Solutions { get; set; } = [];
    public virtual List<CourseReviewModel> Reviews { get; set; } = [];

    public virtual List<FileModel> Files { get; set; } = [];

    public TutorProfileModel TutorProfile { get; set; } = null!;
}