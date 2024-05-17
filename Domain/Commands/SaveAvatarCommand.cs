using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mime;
using AutoMapper;
using Domain.Helpers;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Domain.Commands;

public class SaveAvatarCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public IFormFile FormFile { get; set; } = null!;

    public class SaveAvatarCommandHandler : BaseMediatrHandler<SaveAvatarCommand, bool>
    {
        private readonly IHostingEnvironment _environment;

        public SaveAvatarCommandHandler(AppDbContext dbContext, IMapper mapper,
            IHostingEnvironment environment)
            : base(dbContext, mapper) =>
            _environment = environment;

        //https://blog.elmah.io/upload-and-resize-an-image-with-asp-net-core-and-imagesharp/
        public override Task<bool> Handle(SaveAvatarCommand r, CancellationToken token)
        {
            using (var fs = new FileStream(Path.Combine(_environment.WebRootPath, "avatars", $"{r.UserId}.png"),
                       FileMode.Create))
            using (var image = Image.Load(r.FormFile.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(200, 200));
                image.Save(fs, new PngEncoder());
            }

            return Task.FromResult(true);
        }
    }
}