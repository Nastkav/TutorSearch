using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Models;

namespace Domain.Models;

public class TutorEditDto
{
    [MaxLength(400)] public string ImgPath { get; set; } = string.Empty;

    [DataType(DataType.Text), Length(10, 150), Display(Name = "Address")]
    public string Address { get; set; } = string.Empty;

    public int CityId { get; set; }
    public string? City { get; set; }
    [Length(3, 100)] public string Experience { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false), Length(3, 300)]
    public string Descriptions { get; set; } = string.Empty;

    [Length(3, 1000)] public string About { get; set; } = string.Empty;

    //Work
    [DataType(DataType.Currency), Display(Name = "Rate per hour"), Range(10, 9999)]

    public decimal HourRate { get; set; }

    public bool OnlineAccess { get; set; }
    public bool TutorHomeAccess { get; set; }
    public bool StudentHomeAccess { get; set; }

    //Subjects
    public Dictionary<int, string> Subjects { get; set; } = new();
}