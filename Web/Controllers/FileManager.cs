using System.Security.Claims;
using Domain.Commands;
using Domain.Models;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Web.Models.Shared;

namespace Web.Controllers;

public class FilesController : Controller
{
    private readonly IMediator _mediator;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public FilesController(IMediator mediator) => _mediator = mediator;

    public async Task<IActionResult> Download(string id)
    {
        if (id.Length != 36) //Довжина ключа GUID
            return Content("filename is not available");
        var file = await _mediator.Send(new DownloadFileCommand() { UserId = IdentityId, FileId = id });
        if (file != null)
        {
            string? contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(file.Filename, out contentType);
            return File(file.Memory, contentType ?? "", file.Filename);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Upload(UploadFileCommand command)
    {
        try
        {
            if (command.UserId == null)
                command.UserId = IdentityId;
            else if (command.UserId != IdentityId)
                throw new Exception("Користувач вказаний невірно");

            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteFileCommand command)
    {
        if (command.FileId.Length != 36) //Довжина ключа GUID
            throw new Exception("Невірний формат ідентіфактору файлу");
        command.UserId = IdentityId;
        await _mediator.Send(command);
        return Ok(true);
    }
}