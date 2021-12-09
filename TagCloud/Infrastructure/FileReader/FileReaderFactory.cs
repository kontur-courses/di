using TagCloud.App.UI.Common;

namespace TagCloud.Infrastructure.FileReader;

public class FileReaderFactory : IFileReaderFactory
{
    private readonly IFileReader defaultFileReader;

    public readonly IReadOnlyDictionary<string, IFileReader> FileReaders;

    public FileReaderFactory(IEnumerable<IFileReader> fileReaders, IFileReader defaultFileReader)
    {
        this.defaultFileReader = defaultFileReader;
        FileReaders = CreateExtensionsDictionary(fileReaders);
    }

    public IFileReader Create(IInputPathProvider inputPathProvider)
    {
        return Create(inputPathProvider.InputPath);
    }

    public IFileReader Create(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        var extension = fileInfo.Extension;

        return FileReaders.ContainsKey(extension)
            ? FileReaders[extension]
            : defaultFileReader;
    }

    private Dictionary<string, IFileReader> CreateExtensionsDictionary(IEnumerable<IFileReader> fileReaders)
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