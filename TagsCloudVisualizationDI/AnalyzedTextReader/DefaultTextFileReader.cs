using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.AnalyzedTextReader
{
    public class DefaultTextFileReader : ITextFileReader
    {
        public DefaultTextFileReader(string preAnalyzedTextPath, Encoding encoding)
        {
            PreAnalyzedTextPath = preAnalyzedTextPath;
            ReadingEncoding = encoding;
        }


        public string PreAnalyzedTextPath { get; }
        public Encoding ReadingEncoding { get; }


        public string[] ReadText()
        {
            if (File.Exists(PreAnalyzedTextPath))
            {
                return File.ReadAllLines(PreAnalyzedTextPath, ReadingEncoding);
            }

            throw new FileNotFoundException($"Giving path {PreAnalyzedTextPath} is not valid");
        }
    }
}
