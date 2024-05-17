using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Models;

namespace Domain.Models;

public class UserFile
{
    [MaxLength(36)] public Guid Id { get; set; }
    [MaxLength(100)] public string FileName { get; set; } = string.Empty;

    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = null!;
}