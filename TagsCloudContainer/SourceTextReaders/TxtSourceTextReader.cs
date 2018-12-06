using System.IO;

namespace TagsCloudContainer.SourceTextReaders
{
    public class TxtSourceTextReader : ISourceTextReader
    {
        public string[] ReadText(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
