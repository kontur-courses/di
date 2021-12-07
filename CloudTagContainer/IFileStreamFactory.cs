using System.IO;

namespace CloudTagContainer
{
    public interface IFileStreamFactory
    {
        public Stream Open(string path);
    }
}