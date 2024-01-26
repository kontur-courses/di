using System.Drawing;

namespace TagsCloud.Entities;

public class Tag
{
    public RectangleF TagRectangle;
    public  Color Color;
    public  string Content;
    public Font Font;
    public SizeF size;

    public Tag(Point center , Color color, Font font,string content)
    {
        //this.TagInfo = tagInfo;
        this.Color = color;
        this.Content = content;
        this.Font = font;

        using (Graphics g = Graphics.FromImage(new Bitmap(1,1)))
        {
            size = g.MeasureString(content, font);
        }
        TagRectangle = new RectangleF(center.X, center.Y, size.Width, size.Height);
    }
    
    
    
}