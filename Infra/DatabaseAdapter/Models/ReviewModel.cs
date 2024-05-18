using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter.Models;

public class ReviewModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorModel Tutor { get; set; } = null!;
    [Range(0, 10)] public int Rating { get; set; }
    [MaxLength(1000)] public string Description { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public UserModel Author { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}