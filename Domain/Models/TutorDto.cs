namespace Domain.Models;

public class TutorDto : TutorEditDto
{
    public int Id { get; set; }
    public bool Enabled { get; set; } = false; 

    public float RatingValue { get; set; }
    public int ReviewCount { get; set; }
}