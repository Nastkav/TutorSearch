using Domain.Models;

namespace Domain.Models;

public class TutorRatingDto
{
    public TutorRatingDto(float average = 0, int count = 0)
    {
        Average = average;
        Count = count;
        ListReviews = new List<ReviewDto>();
    }

    public float Average { get; set; }
    public int Count { get; set; }
    public List<ReviewDto> ListReviews { get; set; }
}