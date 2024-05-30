namespace Web.Models.Shared;

public class ErrorVm
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
}