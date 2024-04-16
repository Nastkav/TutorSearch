namespace Web.Models.TutorProfile;

public class TutorCardViewModel
{
    public int Id { get; set; }
    public bool Enabled { get; set; } = false;
    public string ImgPath { get; set; } //TODO:ImgPath
    public decimal HourRate { get; set; } //TODO:HourRate
    public string Experience { get; set; } //TODO:Experience
    public string Descriptions { get; set; }
    public string City { get; set; } //TODO:City

    public List<string> Subjects { get; set; } = new();

    public string ReviewString { get; set; }
    public float ReviewValue { get; set; }
    public int ReviewCount { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}