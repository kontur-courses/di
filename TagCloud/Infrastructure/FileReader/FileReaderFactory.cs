using System.Linq.Expressions;
using TagCloud.App.UI.Common;

namespace TagCloud.Infrastructure.FileReader;

public class FileReaderFactory : IFileReaderFactory
{
    public static readonly IReadOnlyDictionary<string, Type> ReaderTypes = new Dictionary<string, Type>
    {
        { ".doc", typeof(DocFileReader) },
        { ".docx", typeof(DocFileReader) }
    };

    public IFileReader Create(IInputPathProvider inputPathProvider)
    {
        return Create(inputPathProvider.InputPath);
    }

    public IFileReader Create(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        var extension = fileInfo.Extension;

        if (ReaderTypes.ContainsKey(extension))
            return (IFileReader) Activator.CreateInstance(ReaderTypes[extension]);

        return new PlainTextFileReader();
    }
}