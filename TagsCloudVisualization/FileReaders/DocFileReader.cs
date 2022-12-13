using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace TagsCloudVisualization.FileReaders
{
    public class DocFileReader : IFileReader
    {
        public string FilePath { get; }

        public DocFileReader(string path)
        {
            FilePath = path;
        }

        public bool TryReadAllText(out string text)
        {
            text = string.Empty;
            try
            {
                using var document = new WordDocument(FilePath);
                text = document.GetText();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
