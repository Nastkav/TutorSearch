using Domain.Models;

namespace Domain.Models;

public class TutorRating
{
    public TutorRating(float average = 0, int count = 0)
    {
        Average = average;
        Count = count;
        ListReviews = new List<Review>();
    }

    public float Average { get; set; }
    public int Count { get; set; }
    public List<Review> ListReviews { get; set; }
}