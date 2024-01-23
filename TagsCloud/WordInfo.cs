using System.Windows.Forms;

namespace TagsCloud;

public class WordInfo
{
    public readonly int Count;
    public readonly string Word;

    private WordInfo(string word, int count)
    {
        Word = word;
        Count = count;
    }

    public static WordInfo Create(string word, int count)
    {
        return new WordInfo(word, count);
    }
}