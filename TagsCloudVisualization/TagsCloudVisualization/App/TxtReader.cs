using System.IO;

namespace TagsCloudVisualization
{
    public class TxtReader : IFileReader
    {
        public string Read(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}
