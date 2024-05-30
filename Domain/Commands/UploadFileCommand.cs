using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mime;
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
    public int? AssignmentId { get; set; }
    public int? SolutionId { get; set; }
    public byte[] FileBytes { get; set; } = [];
    public string FileName { get; set; } = string.Empty;

    public class UploadFileCommandHandler : BaseMediatrHandler<UploadFileCommand, bool>
    {
        private readonly IStorageRepository _storageRepo;

        public UploadFileCommandHandler(AppDbContext dbContext, IMapper mapper, IStorageRepository storageRepo) :
            base(dbContext, mapper) => _storageRepo = storageRepo;

        //https://blog.elmah.io/upload-and-resize-an-image-with-asp-net-core-and-imagesharp/
        public override async Task<bool> Handle(UploadFileCommand r, CancellationToken token)
        {
            //Перевірки
            if (!r.UserId.HasValue || r.UserId == 0)
                throw new UserNotFoundException("Користувач вказаний невірно");
            if (r.FileBytes.Length == 0)
                throw new CommandParameterException("Передача файлу обов'язкова");
            if (r.FileName == string.Empty)
                throw new CommandParameterException("Файл повинен мати назву");

            var dbAssignment = await DatabaseContext.Assignments
                .FirstOrDefaultAsync(x => x.Id == r.AssignmentId && x.TutorId == r.UserId);
            var dbSolution = await DatabaseContext.Solutions
                .Include(x => x.Assignment)
                .FirstOrDefaultAsync(x =>
                    (x.Id == r.SolutionId && x.StudentId == r.UserId) || x.Assignment.TutorId == r.UserId);

            if (r.AssignmentId != null && dbAssignment == null)
                throw new AssignmentException("Невірно вказано номер завдання");
            if (r.SolutionId != null && dbSolution == null)
                throw new SolutionException("Невірно вказано номер рішення");

            var newFile = _storageRepo.SaveFile(Path.GetExtension(r.FileName), r.FileBytes);
            //Реєстрація нового рядка в БД
            var dbFile = new UserFileModel()
            {
                Id = newFile.Guid,
                FileName = r.FileName,
                ServerName = newFile.Filename,
                OwnerId = r.UserId.Value
            };

            //Посилання на файли із завданнями та рішеннями
            if (r.AssignmentId.HasValue && dbAssignment != null)
                dbFile.Assignments.Add(dbAssignment);
            if (r.SolutionId.HasValue && dbSolution != null)
                dbFile.Solutions.Add(dbSolution);

            //Зберегти зміни в новому рядку в базі даних
            DatabaseContext.Files.Add(dbFile);
            await DatabaseContext.SaveChangesAsync();
            return true;
        }
    }
}