namespace TagCloudCreator.Infrastructure.Settings;

public class FileBlobStorage : IBlobStorage
{
    public byte[]? Get(string name) => 
        File.Exists(name) ? File.ReadAllBytes(name) : null;

    public void Set(string name, byte[] content) => 
        File.WriteAllBytes(name, content);
}