using System.IO;

namespace TagCloud.Infrastructure
{
    public interface IFileInfoProvider
    {
        FileInfo GetFileInfo(string path);
    }
}
