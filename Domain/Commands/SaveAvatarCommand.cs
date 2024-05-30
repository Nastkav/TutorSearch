using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Domain.Helpers;
using Infra;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Infra.Storage;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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

    public byte[] ImageBytes { get; set; } = [];

    public class SaveAvatarCommandHandler : IRequestHandler<SaveAvatarCommand, bool>
    {
        private readonly IStorageRepository _storage;

        public SaveAvatarCommandHandler(IStorageRepository storage) =>
            _storage = storage;

        public Task<bool> Handle(SaveAvatarCommand r, CancellationToken token)
        {
            if (r.ImageBytes.Length == 0)
                throw new CommandParameterException("Файл не може бути пустим");

            _storage.SaveAvatar(r.UserId.ToString(), r.ImageBytes);
            return Task.FromResult(true);
        }
    }
}