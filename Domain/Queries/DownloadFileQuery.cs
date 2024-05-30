using AutoMapper;
using Domain.Helpers;
using Infra;
using Infra.DatabaseAdapter;
using Infra.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Domain.Queries;

public class DownloadFileQuery : IRequest<StorageFile>
{
    public string FileId { get; set; } = string.Empty;
    public int UserId { get; set; }

    public class DownloadFileQueryHandler : BaseMediatrHandler<DownloadFileQuery, StorageFile>
    {
        private readonly IStorageRepository _storageRepo;


        public DownloadFileQueryHandler(AppDbContext dbContext, IMapper mapper, IStorageRepository storageRepo) :
            base(dbContext, mapper) => _storageRepo = storageRepo;


        public override async Task<StorageFile> Handle(DownloadFileQuery r, CancellationToken token)
        {
            var dbFile = await DatabaseContext.Files.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == r.FileId);
            if (dbFile == null)
                throw new FileNotFoundException("Файл не знайдено у базі даних");

            var isOwner = dbFile.OwnerId == r.UserId && r.UserId != 0;
            var studentAccess = await DatabaseContext.Files.AsNoTracking() //доступ до студентів до файлів вчителів
                .Include(x => x.Assignments)
                .ThenInclude(x => x.Solutions)
                .AnyAsync(f => f.Assignments.Any(a => a.Solutions.Any(s => s.StudentId == r.UserId)));
            var tutorAccess = await DatabaseContext.Files.AsNoTracking() // доступ вчителів для файлів студентів 
                .Include(x => x.Solutions)
                .ThenInclude(x => x.Assignment)
                .AnyAsync(f => f.Solutions.Any(s => s.Assignment.TutorId == r.UserId));

            if (!(isOwner || studentAccess || tutorAccess)) // якщо користувач не має ні одного типу доступу
                throw new AccessDeniedException("Доступ до файлу обмежений");

            var file = _storageRepo.GetFile(dbFile.ServerName);
            return file;
        }
    }
}