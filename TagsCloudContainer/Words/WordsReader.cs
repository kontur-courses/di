using TagsCloudContainer.WordsInterfaces;

namespace TagsCloudContainer;

public class WordsReader : IWordsReader
{
    public List<string> Read(string? path)
    {
        var f = new StreamReader(path);
        var result = new List<string>();
        while (!f.EndOfStream)
            result.Add(f.ReadLine());

        f.Close();
        return result;
    }
}