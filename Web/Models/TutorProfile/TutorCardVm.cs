namespace Web.Models.TutorProfile;

public class TutorCardVm
{
    public string ImgPath { get; set; } = string.Empty;
    public string GetImgPath() => ImgPath != string.Empty ? ImgPath : "/img/example_face.jpg";
    public decimal HourRate { get; set; }

    public string Descriptions { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public List<string> Subjects { get; set; } = new();

    public float ReviewValue { get; set; }
    public int ReviewCount { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;

    public int Id { get; set; }
}