using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

// [Table("TutorProfile")]
public class TutorProfileModel : ITrackable
{
    public int Id { get; set; }

    [Required] public AboutTutorModel About { get; set; } = new();

    [Required(AllowEmptyStrings = false), MaxLength(400)]

    public string ImgPath { get; set; } = string.Empty;

    public bool Enabled { get; set; } = false;
    [MaxLength(100)] public string Address { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false), MaxLength(100)]
    public string Experience { get; set; } = string.Empty;

    [Required, MaxLength(300)] public string Descriptions { get; set; } = string.Empty;

    public int? CityId { get; set; }
    public CityModel? City { get; set; }

    [DataType(DataType.Currency), Range(10, 9999)]
    public decimal HourRate { get; set; }

    public bool OnlineAccess { get; set; }
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }

    public virtual List<SubjectModel> Subjects { get; set; } = [];

    public virtual List<AvailableTime> AvailableTimes { get; set; } = [];
    public virtual List<CourseModel> Courses { get; set; } = [];
    public virtual List<LessonModel> TeachingLessons { get; set; } = [];


    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedId { get; set; }
    public UserModel Created { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedId { get; set; }
}