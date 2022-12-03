using System.Collections;

namespace TagsCloudContainer;

public class WordsFromFileReader : IEnumerable<string>
{
    public List<string> Words;

    public WordsFromFileReader(string path)
    {
        Words = new List<string>(File.ReadAllLines(path));
    }

    public IEnumerator<string> GetEnumerator()
    {
        return Words.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}