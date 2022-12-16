using System.Text;

namespace TagCloudPainter.FileReader;

public class TxtReader : IFileReader
{
    public IEnumerable<string> ReadFile(string path)
    {
        var words = new List<string>();
        using (var reader = new StreamReader(path, Encoding.UTF8))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
                if(line != "")
                    words.Add(line);
        }

        return words;
    }
}