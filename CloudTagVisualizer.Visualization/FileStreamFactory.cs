using System.IO;

namespace Visualization
{
    public class FileStreamFactory : IFileStreamFactory
    {
        public Stream OpenOnWriting(string path)
        {
            return new FileStream(path, FileMode.OpenOrCreate);
        }

        public Stream OpenOnReading(string path)
        {
            return new FileStream(path, FileMode.Open);
        }
    }
}