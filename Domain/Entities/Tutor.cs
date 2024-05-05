using System.Runtime.InteropServices.JavaScript;

namespace Domain.Entities;

public class Tutor
{
    public int Id { get; set; }
    public string ImgPath { get; set; } = "";
    public string Address { get; set; } = "";
    public int City { get; set; }
    public string Experience { get; set; } = "";
    public string Descriptions { get; set; } = "";
    public decimal HourRate { get; set; }
    public bool OnlineAccess { get; set; } = false;
    public bool TutorHomeAccess { get; set; } = false;
    public bool StudentHomeAccess { get; set; } = false;
    public Dictionary<int, string> Subjects { get; set; } = new();
    public List<Review> ListReviews { get; set; } = new();
}