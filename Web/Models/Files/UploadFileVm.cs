namespace Web.Models.Files;

public class UploadFileVm
{
    public int? UserId { get; set; }
    public IFormFile FormFile { get; set; } = null!;
    public int? AssignmentId { get; set; }
    public int? SolutionId { get; set; }
}