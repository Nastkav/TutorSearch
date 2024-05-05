using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class CityModel : ITrackable
{
    public int Id { get; set; }

    [DisplayName("Назва"), MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(140), DisplayName("Область")]
    public string Region { get; set; } = string.Empty;

    //ITrackable
    [DisplayName("Додано")] public DateTime CreatedAt { get; set; }
    [DisplayName("Створив")] public int CreatedId { get; set; }
    [DisplayName("Оновлено в")] public DateTime? UpdatedAt { get; set; }
    [DisplayName("Хто оновив")] public int? UpdatedId { get; set; }


    public string FullName() => $"{Name},{Region}";
}