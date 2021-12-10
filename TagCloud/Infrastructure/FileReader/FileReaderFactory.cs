namespace TagCloud.Infrastructure.FileReader;

public class FileReaderFactory : IFileReaderFactory
{
    private readonly IFileReader defaultFileReader;

    private readonly IReadOnlyDictionary<string, IFileReader> fileReaders;

    public FileReaderFactory(IEnumerable<IFileReader> fileReaders, IFileReader defaultFileReader)
    {
        this.defaultFileReader = defaultFileReader;
        this.fileReaders = CreateExtensionsDictionary(fileReaders);
    }

    public IFileReader Create(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        var extension = fileInfo.Extension;

        return fileReaders.ContainsKey(extension)
            ? fileReaders[extension]
            : defaultFileReader;
    }

    private static Dictionary<string, IFileReader> CreateExtensionsDictionary(IEnumerable<IFileReader> fileReaders)
    {
        var dictionary = new Dictionary<string, IFileReader>();

        foreach (var fileReader in fileReaders)
        {
            foreach (var extension in fileReader.GetSupportedExtensions())
            {
                dictionary[extension] = fileReader;
            }
        }

        return dictionary;
    }
}