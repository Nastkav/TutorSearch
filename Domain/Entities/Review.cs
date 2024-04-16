namespace Domain.Entities;

public class Review
{
    public Review(int id, int tutorId, int authorId, int rating, string comment, DateTime createdAt)
    {
        Id = id;
        TutorId = tutorId;
        AuthorId = authorId;
        Rating = rating;
        Comment = comment;
        CreatedAt = createdAt;
    }

    public int Id { get; set; }
    public int TutorId { get; set; }
    public int AuthorId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}