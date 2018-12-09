using System.IO;

namespace TagsCloudContainer.SourceTextReaders
{
    public class TxtSourceTextReader : ISourceTextReader
    {
        private readonly string filePath;

        public TxtSourceTextReader(string filePath)
        {
            this.filePath = filePath;
        }
        
        public string[] ReadText()
        {
            return File.ReadAllLines(filePath);
        }
    }
}
