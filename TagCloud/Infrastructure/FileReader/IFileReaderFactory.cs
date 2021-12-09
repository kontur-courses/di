using TagCloud.App.UI.Common;

namespace TagCloud.Infrastructure.FileReader;

public interface IFileReaderFactory
{
    IFileReader Create(IInputPathProvider inputPathProvider);
    IFileReader Create(string filePath);
}