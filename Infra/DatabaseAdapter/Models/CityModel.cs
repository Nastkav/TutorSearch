using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class CityModel : ITrackable
{
    public int Id { get; set; }

    [DisplayName("Назва")]
    [MaxLength(100)]
    [Required(ErrorMessage = "Поле '{0}' є обов'язковим")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(140)]
    [DisplayName("Область")]
    [Required(ErrorMessage = "Поле '{0}' є обов'язковим")]
    public string Region { get; set; } = string.Empty;

    //ITrackable
    [DisplayName("Додано")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime CreatedAt { get; set; }

    [DisplayFormat(DataFormatString = "{0:d}")]
    [DisplayName("Оновлено в")]
    public DateTime? UpdatedAt { get; set; }


    public string FullName() => $"{Name},{Region}";
}