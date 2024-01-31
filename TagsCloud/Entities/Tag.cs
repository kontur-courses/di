using System.Drawing;

namespace TagsCloud.Entities;

public class Tag
{
    public Rectangle TagRectangle;
    public  string Content;
    public Font Font;

    public Tag(Rectangle tagRectangle, Font font,string content)
    {
        //this.TagInfo = tagInfo;
        this.Content = content;
        this.TagRectangle = tagRectangle;
        this.Font = font;
    }
    
    
    
}