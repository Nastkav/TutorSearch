using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Models;

namespace Domain.Models;

public class UserFile
{
    [MaxLength(36)] public string Id { get; set; } = string.Empty;
    [MaxLength(100)] public string FileName { get; set; } = string.Empty;

    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = null!;
}