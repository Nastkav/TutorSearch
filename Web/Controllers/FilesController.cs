using System.Diagnostics;
using System.Security.Claims;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Humanizer;
using Infra;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Web.Helpers;
using Web.Models.Files;
using Web.Models.Shared;

namespace Web.Controllers;

[Authorize]
[Route("/[controller]/[action]")]
public class FileController : Controller
{
    private readonly IMediator _mediator;
    public int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

    public FileController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Download(string id)
    {
        if (id.Length != 36) //Довжина ключа GUID
            return Content("filename is not available");
        try
        {
            var file = await _mediator.Send(new DownloadFileQuery() { UserId = IdentityId, FileId = id });
            string? contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(file.Filename, out contentType);
            return File(file.Memory, contentType ?? "", file.Filename);
        }
        catch
        {
            return NotFound();
        }
    }


    [HttpPost]
    public async Task<IActionResult> Upload(UploadFileVm model)
    {
        var helper = new ControllerHelpers(_mediator);
        try
        {
            if (model.UserId == null)
                model.UserId = IdentityId;
            else if (model.UserId != IdentityId)
                throw new Exception("Користувач вказаний невірно");
            var command = new UploadFileCommand()
            {
                UserId = model.UserId,
                AssignmentId = model.AssignmentId,
                SolutionId = model.SolutionId,
                FileBytes = await helper.BytesFromFormFile(model.FormFile),
                FileName = model.FormFile.FileName
            };
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}