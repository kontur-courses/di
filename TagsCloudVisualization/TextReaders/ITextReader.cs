using System.Text;

namespace TagsCloudVisualization.TextReaders
{
    public interface ITextReader
    {
        string ReadText(string filePath, Encoding encoding);
    }
}