using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DatabaseAdapter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter.Models;

public class TutorReviewModel : ITrackable
{
    public int Id { get; set; }
    public int TutorId { get; set; }
    public TutorModel Tutor { get; set; } = null!;
    [Range(0, 5)] public int Rating { get; set; }
    [MaxLength(300)] public string Description { get; set; } = string.Empty;

    public int CreatedId { get; set; }
    public UserModel Created { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}