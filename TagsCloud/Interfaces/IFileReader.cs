using System.IO;

namespace TagsCloud
{
    interface IFileReader
    {
        FileStream ReadFile(string path);
    }
}