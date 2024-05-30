namespace Infra.Storage;

public interface IStorageRepository
{
    public void RemoveFileIfExist(string filename);
    public StorageFile SaveFile(string extension, byte[] data);
    public void SaveAvatar(string filename, byte[] data);
    public StorageFile GetFile(string serverName);
}