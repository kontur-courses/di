using System.IO;

namespace Visualization
{
    public interface IFileStreamFactory
    {
        public Stream OpenOnWriting(string path);
        public Stream OpenOnReading(string path);
    }
}