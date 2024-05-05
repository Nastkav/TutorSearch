namespace Domain.Models;

public class ReviewDto
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public int AuthorId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}