using System.Text;
using TagsCloudContainer.FileReader.Interfaces;

namespace TagsCloudContainer.FileReader;
public class TxtFileReader : ITextReader
{
    public string GetTextFromFile(string filePath)
    {
        var sb = new StringBuilder();
        using (var sr = new StreamReader(filePath))
        {
            var words = sr.ReadLine();
            while (words != null)
            {
                sb.Append(words + Environment.NewLine);
                words = sr.ReadLine();
            }
        }

        return sb.ToString();
    }
}