using System.IO;

namespace TagsCloud.Interfaces
{
    public interface IFileReader
    {
        string ReadFile(string path);
    }
}