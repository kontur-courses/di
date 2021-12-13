using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public interface ITextFileReader
    {
        public string PreAnalyzedTextPath { get;}

        public Encoding ReadingEncoding { get; }
        string[] ReadText();
    }
}
