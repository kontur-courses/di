using System.Drawing;

namespace TagCloud.Words;

public class WordRectangle
{
    public WordRectangle(Word word)
    {
        Word = word;
    }

    public Word Word { get; }

    public Rectangle Rectangle { get; set; }
}