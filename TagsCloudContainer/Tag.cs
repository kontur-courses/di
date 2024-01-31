using System.Drawing;

namespace TagsCloudContainer;

public struct Tag
{
    public string Word { get; }
    
    public Rectangle Rectangle { get; }
    
    public Tag(Rectangle rectangle, string word)
    {
        Rectangle = rectangle;
        Word = word;
    }
}