using Domain.Models;

namespace Domain.Models;

public class TutorRating
{
    public float Average { get; set; }
    public int Count { get; set; }
    public List<Review> ListReviews { get; set; } = [];
}