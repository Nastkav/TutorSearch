using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Commands;

public class DownloadFileCommand : IRequest<DownloadFileCommand.StorageFile?>
{
    public class StorageFile
    {
        public readonly MemoryStream Memory;
        public readonly string Filename;

        public StorageFile(MemoryStream memory, string filename)
        {
            Memory = memory;
            Filename = filename;
        }
    }

    public string FileId { get; set; } = string.Empty;
    public int UserId { get; set; }

    public class DownloadFileCommandHandler : BaseMediatrHandler<DownloadFileCommand, StorageFile?>
    {
        private readonly string _filesFolder;
        private readonly ILogger<DownloadFileCommandHandler> _logger;

        public override async Task<StorageFile?> Handle(DownloadFileCommand r, CancellationToken token)
        {
            try
            {
                var dbFile = await DatabaseContext.Files.FirstOrDefaultAsync(x => x.Id.ToString() == r.FileId);
                if (dbFile == null)
                    throw new Exception("Файл не знайдено у базі даних");

                var isOwner = dbFile.OwnerId == r.UserId;
                var studentAccess = await DatabaseContext.Files //доступ до студентів до файлів вчителів
                    .Include(x => x.Assignments)
                    .ThenInclude(x => x.Solutions)
                    .AnyAsync(f => f.Assignments.Any(a => a.Solutions.Any(s => s.StudentId == r.UserId)));
                var tutorAccess = await DatabaseContext.Files // доступ вчителів для файлів студентів 
                    .Include(x => x.Solutions)
                    .ThenInclude(x => x.Assignment)
                    .AnyAsync(f => f.Solutions.Any(s => s.Assignment.TutorId == r.UserId));

                if (!(isOwner || studentAccess || tutorAccess)) // якщо користувач не має ні одного типу доступу
                    throw new Exception("Доступ до файлу обмежений");

                // Перевірка, чи існує файл 
                var path = Path.Combine(_filesFolder, dbFile.ServerName);
                if (!File.Exists(path))
                    throw new Exception("File not found in storage");


                //Копирование файла в память
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open)) stream.CopyTo(memory);
                memory.Position = 0;
                return new StorageFile(memory, dbFile.ServerName);
            }
            catch (IOException ioExp)
            {
                _logger.LogError(ioExp.Message);
            }

            return null;
        }

        public DownloadFileCommandHandler(AppDbContext dbContext, IMapper mapper, IConfiguration configuration,
            ILogger<DownloadFileCommandHandler> logger) : base(dbContext, mapper)
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
    }
}