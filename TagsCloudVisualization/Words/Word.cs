using System.Drawing;

namespace TagsCloudVisualization.Words;

public class Word
{
    public string Text { get; private set; }
    public int Count { get; private set; }
    public float Size { get; private set; }


    public Word(string text, int count, float size)
    {
        Text = text;
        Count = count;
        Size = size;
    }
}