using System.Configuration;
using System.Data;
using Infra.DatabaseAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Infra;
using Infra.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Commands;

public class DeleteFileCommand : IRequest<bool>
{
    public string FileId { get; set; } = string.Empty;
    public int? UserId { get; set; }

    public class DeleteFileCommandHandler : BaseMediatrHandler<DeleteFileCommand, bool>
    {
        private readonly IStorageRepository _storageRepo;

        public DeleteFileCommandHandler(AppDbContext dbContext, IMapper mapper, IStorageRepository storageRepo) :
            base(dbContext, mapper) => _storageRepo = storageRepo;


        public override async Task<bool> Handle(DeleteFileCommand r, CancellationToken token)
        {
            var dbFile = await DatabaseContext.Files.FirstOrDefaultAsync(x => x.Id.ToString() == r.FileId);
            if (dbFile == null)
                throw new CommandParameterException("Файл не знайдено у базі даних");
            if (r.UserId == 0 || dbFile.OwnerId != r.UserId)
                throw new CommandParameterException("Тільки власник може видалити файл");

            _storageRepo.RemoveFileIfExist(dbFile.ServerName);

            //Видалення файлу з БД
            DatabaseContext.Remove(dbFile);
            await DatabaseContext.SaveChangesAsync();
            return true;
        }
    }
}