using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Commands;

public class DeleteFileCommand : IRequest<bool>
{
    public string FileId { get; set; } = string.Empty;
    public int? UserId { get; set; }

    public class DeleteFileCommandHandler : BaseMediatrHandler<DeleteFileCommand, bool>
    {
        private readonly string _filesFolder;
        private readonly ILogger<DeleteFileCommandHandler> _logger;

        public override async Task<bool> Handle(DeleteFileCommand r, CancellationToken token)
        {
            try
            {
                var dbFile = await DatabaseContext.Files.FirstOrDefaultAsync(x => x.Id.ToString() == r.FileId);
                if (dbFile == null)
                    throw new Exception("Файл не знайдено у базі даних");
                if (dbFile.OwnerId != r.UserId)
                    throw new Exception("Тільки власник може видалити файл");

                // Перевірка, чи існує файл із повним шляхомкщо файл знайдено, видаліть його
                if (File.Exists(Path.Combine(_filesFolder, dbFile.ServerName)))
                    File.Delete(Path.Combine(_filesFolder, dbFile.ServerName));

                //Видалення файлу з БД
                DatabaseContext.Remove(dbFile);
                await DatabaseContext.SaveChangesAsync();
                return true;
            }
            catch (IOException ioExp)
            {
                _logger.LogError(ioExp.Message);
            }

            return false;
        }

        public DeleteFileCommandHandler(AppDbContext dbContext, IMapper mapper, IConfiguration configuration,
            ILogger<DeleteFileCommandHandler> logger) : base(dbContext, mapper)
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