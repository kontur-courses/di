using System.IO;

namespace TagsCloudContainer.Input
{
    public class TxtReader : IFileReader
    {
        public string Read(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}