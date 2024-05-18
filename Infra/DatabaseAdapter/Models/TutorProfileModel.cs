using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class TutorModel : ITrackable
{
    public int Id { get; set; }

    public UserModel User { get; set; } = null!;

    [Required] public AboutTutorModel About { get; set; } = new();
    [MaxLength(300)] public string Address { get; set; } = string.Empty;

    public bool OnlineAccess { get; set; }
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }


    [MaxLength(300)] public string Descriptions { get; set; } = string.Empty;


    [DataType(DataType.Currency)]
    [Range(10, 9999)]
    public decimal HourRate { get; set; }

    public virtual List<SubjectModel> Subjects { get; set; } = [];
    public virtual List<AvailableTimeModel> AvailableTimes { get; set; } = [];
    public virtual List<RequestModel> Requests { get; set; } = [];
    public virtual List<LessonModel> TeachingLessons { get; set; } = [];

    public virtual List<ReviewModel> Reviews { get; set; } = [];

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}