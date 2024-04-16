using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter.Models;

public class CourseReview
{
    public int Id { get; set; }
    public Guid CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;
    [Range(0, 5)] public int Rating { get; set; }
    [MaxLength(300)] public string Description { get; set; } = string.Empty;

    [ForeignKey("CreatedBy")] public UserModel Owner { get; set; } = null!;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}