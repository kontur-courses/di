using TagCloud.Core.FileReaders;

namespace TagCloudUI.Infrastructure.Selectors
{
    public interface IReaderSelector
    {
        public bool TryGetReader(string extension, out IFileReader reader);
    }
}