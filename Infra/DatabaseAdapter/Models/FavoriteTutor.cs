using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseAdapter.Models;

public class FavoriteTutorModel
{
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public int ProfileId { get; set; }
    public TutorModel Profile { get; set; } = null!;
}