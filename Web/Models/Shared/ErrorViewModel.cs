namespace Web.Models.Shared;

public class ErrorVm
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string Message { get; set; } = string.Empty;
}