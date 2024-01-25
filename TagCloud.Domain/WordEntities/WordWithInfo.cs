namespace TagCloud.Domain.WordEntities;

public class WordWithInfo : WordWithCount
{
    public WordWithInfo(WordWithCount word, Rectangle rectangle, Font font)
     : base(word.Text, word.Count)
    {
        Rectangle = rectangle;
        Font = font;
    }
    
    public Rectangle Rectangle { get; }
    public Font Font { get; }
}