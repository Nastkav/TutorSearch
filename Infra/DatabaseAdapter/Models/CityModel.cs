using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class CityModel : ITrackable
{
    public int Id { get; set; }
    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    //ITrackable
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}