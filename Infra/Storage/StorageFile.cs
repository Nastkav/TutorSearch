namespace Infra.Storage;

public class StorageFile
{
    public readonly MemoryStream Memory;
    public readonly string Filename;
    public Guid Guid;

    public StorageFile(MemoryStream memory, string filename, Guid? guid = null)
    {
        Memory = memory;
        Filename = filename;
        if (guid.HasValue)
            Guid = guid.Value;
    }
}