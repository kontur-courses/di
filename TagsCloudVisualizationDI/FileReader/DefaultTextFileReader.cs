using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public class DefaultTextFileReader : ITextFileReader
    {
        public DefaultTextFileReader(string readingTextPath, Encoding enconding)
        {
            PreAnalyzedTextPath = readingTextPath;
            ReadingEncoding = enconding;
        }


        public string PreAnalyzedTextPath { get; }
        public Encoding ReadingEncoding { get; }


        public string[] ReadText(string path, Encoding encoding)
        {
            if (File.Exists(path))
            { 
                return File.ReadAllLines(path, encoding);
            }
            
            throw new FileNotFoundException($"Giving path {path} is not valid");
        }
    }
}
