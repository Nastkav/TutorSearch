using System.ComponentModel.DataAnnotations;

namespace Domain.DrivingPort.Models;

public class TutorEditDto
{
    [MaxLength(400)] public string ImgPath { get; set; } = null!;

    [DataType(DataType.Text), Length(10, 150), Display(Name = "Address")]
    public string? Address { get; set; }

    public string? City { get; set; }
    [Length(3, 100)] public string? Experience { get; set; }

    [Required(AllowEmptyStrings = false), Length(3, 300)]
    public string? Descriptions { get; set; }

    [Length(3, 1000)] public string? About { get; set; }

    //Work
    [DataType(DataType.Currency), Display(Name = "Rate per hour"), Range(10, 9999)]
    public decimal HourRate { get; set; }

    public bool OnlineAccess { get; set; }
    public bool AtHomeAccess { get; set; }
    public bool OffsiteAccess { get; set; }

    //Subjects
    public Dictionary<int, string> Subjects { get; set; } = new();
}