namespace TagCloud.Files;

public class FileReader
{
    public readonly IEnumerable<IFileReader> _fileReaders;
    
    public FileReader(IEnumerable<IFileReader> fileReaders)
    {
        _fileReaders = fileReaders;
    }

    public string ReadAll(string filename)
    {
        var extension = Path.GetExtension(filename);
        var fileReader = _fileReaders.FirstOrDefault(file => file.Extension == extension);
        if (fileReader is not null)
            return fileReader.ReadAll(filename);

        throw new ArgumentException($"This extension {extension} is not supported!");
    }
}