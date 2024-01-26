namespace TagCloud.Domain.WordEntities;

public class WordToVisualize : WordWithCount
{
    public WordToVisualize(WordWithCount word, Rectangle rectangle, Font font)
     : base(word.Text, word.Count)
    {
        Rectangle = rectangle;
        Font = font;
    }
    
    public Rectangle Rectangle { get; }
    public Font Font { get; }
}