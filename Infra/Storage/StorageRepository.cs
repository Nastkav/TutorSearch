using System.Configuration;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Infra.Storage;

public class StorageRepository : IStorageRepository
{
    private readonly string _filesFolder;
    private readonly string _avatarFolder;

    public StorageRepository(IConfiguration configuration)
    {
        _filesFolder = configuration["FilesFolder"] ?? string.Empty;
        _avatarFolder = configuration["AvatarFolder"] ?? string.Empty;
    }


    public void CheckFolder(string filesFolder)
    {
        if (filesFolder == string.Empty || filesFolder.Length == 0)
            throw new ConfigurationErrorsException("The 'FilesFolder' key does not exist in appsettings.json");
        else if (!Directory.Exists(filesFolder))
            throw new DirectoryNotFoundException($"FilesFolder: Directory {filesFolder} does not exist");
    }

    public void RemoveFileIfExist(string filename)
    {
        CheckFolder(_filesFolder);

        // Перевірка, чи існує файл із повним шляхомкщо файл знайдено, видаліть його
        if (File.Exists(Path.Combine(_filesFolder, filename))) File.Delete(Path.Combine(_filesFolder, filename));
    }

    /// <summary>
    /// Return guid Id and filename for saved file 
    /// </summary>
    /// <param name="extension">.pdf</param>
    /// <param name="data"></param>
    /// <returns></returns>
    public StorageFile SaveFile(string extension, byte[] data)
    {
        CheckFolder(_filesFolder);

        if (extension.Length > 0 && extension[0] != '.')
            extension = "." + extension;

        //Згенерувати шлях до файлу
        var guid = Guid.NewGuid();
        var serverFile = new StorageFile(new MemoryStream(data), guid.ToString() + extension, guid);
        var fullPath = Path.Combine(_filesFolder, serverFile.Filename);

        //Змінити та зберегти в папку з файлами
        using (var fs = new FileStream(fullPath, FileMode.Create))
            fs.Write(data, 0, data.Length);

        return serverFile;
    }

    public void SaveAvatar(string userFile, byte[] data)
    {
        CheckFolder(_avatarFolder);

        using (var fs = new FileStream(Path.Combine(_avatarFolder, userFile + ".png"), FileMode.Create))
        using (var image = Image.Load(data))
        {
            image.Mutate(x => x.Resize(200, 200));
            image.Save(fs, new PngEncoder());
        }
    }


    public StorageFile GetFile(string serverName)
    {
        // Перевірка, чи існує файл 
        var path = Path.Combine(_filesFolder, serverName);
        if (!File.Exists(path))
            throw new FileNotFoundException("File not found in storage");

        //Копирование файла в память
        var memory = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open)) stream.CopyTo(memory);
        memory.Position = 0;
        return new StorageFile(memory, serverName);
    }
}