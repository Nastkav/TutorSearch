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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Domain.Commands;

public class UploadFileCommand : IRequest<bool>
{
    public int? UserId { get; set; }
    public IFormFile FormFile { get; set; } = null!;
    public int? AssignmentId { get; set; }
    public int? SolutionId { get; set; }

    public class UploadFileCommandHandler : BaseMediatrHandler<UploadFileCommand, bool>
    {
        private readonly string _filesFolder;
        private readonly ILogger<UploadFileCommandHandler> _logger;

        public UploadFileCommandHandler(AppDbContext dbContext, IMapper mapper, IConfiguration configuration,
            ILogger<UploadFileCommandHandler> logger) : base(dbContext, mapper)
        {
            _logger = logger;

            var fpath = configuration["FilesFolder"];
            if (fpath == null || fpath.Length == 0)
                throw new Exception("The 'FilesFolder' key does not exist in appsettings.json");
            else if (!Directory.Exists(fpath))
                throw new Exception($"FilesFolder: Directory {fpath} does not exist");
            else
                _filesFolder = fpath;
        }


        //https://blog.elmah.io/upload-and-resize-an-image-with-asp-net-core-and-imagesharp/
        public override async Task<bool> Handle(UploadFileCommand r, CancellationToken token)
        {
            //Перевірки
            if (!r.UserId.HasValue || r.UserId == 0)
                throw new Exception("Користувач вказаний невірно");
            if (r.FormFile == null)
                throw new Exception("Передача файлу обов'язкова");
            try
            {
                //Згенерувати шлях до файлу
                var guid = Guid.NewGuid();
                var serverName = guid.ToString() + Path.GetExtension(r.FormFile.FileName);
                var fullPath = Path.Combine(_filesFolder, serverName);

                //Змінити та зберегти в папку з файлами
                using (var fs = new FileStream(fullPath, FileMode.Create))
                using (var uploadedFile = r.FormFile.OpenReadStream())
                    uploadedFile.CopyTo(fs);

                //Реєстрація нового рядка в БД
                var dbFile = new UserFileModel()
                {
                    Id = guid,
                    FileName = r.FormFile.FileName,
                    ServerName = serverName,
                    OwnerId = r.UserId.Value
                };

                //Посилання на файли із завданнями та рішеннями
                if (r.AssignmentId.HasValue)
                    dbFile.Assignments.Add(
                        await DatabaseContext.Assignments.FirstAsync(x =>
                            x.Id == r.AssignmentId && x.TutorId == r.UserId));
                if (r.SolutionId.HasValue)
                    dbFile.Solutions.Add(
                        await DatabaseContext.Solutions.FirstAsync(x =>
                            x.Id == r.AssignmentId && x.StudentId == r.UserId));

                //Зберегти зміни в новому рядку в базі даних
                DatabaseContext.Files.Add(dbFile);
                await DatabaseContext.SaveChangesAsync();
                return true;
            }
            catch (IOException ioExp)
            {
                _logger.LogError(ioExp.Message);
            }

            return false;
        }
    }
}