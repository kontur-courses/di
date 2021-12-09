using System.Linq.Expressions;
using TagCloud.App.UI.Common;

namespace TagCloud.Infrastructure.FileReader;

public class FileReaderFactory : IFileReaderFactory
{
    public static readonly IReadOnlyDictionary<string, Type> ReaderTypes = new Dictionary<string, Type>
    {
        { ".txt", typeof(PlainTextFileReader) },
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
        {
            return Expression
                .Lambda<Func<IFileReader>>(Expression.New(ReaderTypes[extension].GetConstructor(Type.EmptyTypes)))
                .Compile()();
        }

        return new PlainTextFileReader();
    }
}