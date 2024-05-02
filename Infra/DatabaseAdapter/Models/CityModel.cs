using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Helpers;

namespace Infra.DatabaseAdapter.Models;

public class CityModel : ITrackable
{
    public int Id { get; set; }

    [DisplayName("�����"), MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(140), DisplayName("�������")]
    public string Region { get; set; } = string.Empty;

    //ITrackable
    [DisplayName("������")] public DateTime CreatedAt { get; set; }
    [DisplayName("�������")] public int CreatedId { get; set; }
    [DisplayName("�������� �")] public DateTime? UpdatedAt { get; set; }
    [DisplayName("��� ������")] public int? UpdatedId { get; set; }


    public string FullName() => $"{Name},{Region}";
}