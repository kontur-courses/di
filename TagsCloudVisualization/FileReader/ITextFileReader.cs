using System.Text;

namespace TagsCloudVisualization.FileReader
{
    public interface ITextFileReader
    {
        string[] ReadText(string path, Encoding encoding);
    }
}
