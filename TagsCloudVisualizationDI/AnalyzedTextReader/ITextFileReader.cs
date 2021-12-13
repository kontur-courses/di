using System.Text;

namespace TagsCloudVisualizationDI.AnalyzedTextReader
{
    public interface ITextFileReader
    {
        public string PreAnalyzedTextPath { get; }

        public Encoding ReadingEncoding { get; }
        string[] ReadText();
    }
}
