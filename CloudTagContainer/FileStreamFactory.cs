using System.IO;

namespace CloudTagContainer
{
    public class FileStreamFactory: IFileStreamFactory
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