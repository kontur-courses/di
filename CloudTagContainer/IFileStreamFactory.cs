using System.IO;

namespace CloudTagContainer
{
    public interface IFileStreamFactory
    {
        public Stream OpenOnWriting(string path);
        public Stream OpenOnReading(string path);
    }
}