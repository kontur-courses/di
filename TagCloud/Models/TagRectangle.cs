using System.Drawing;

namespace TagCloud.Models
{
    public class TagRectangle
    {
        public Rectangle Area {get;}
        public string Text { get; }
        public TagRectangle(string text,Rectangle area)
        {
            Area = area;
            Text = text;
        }
    }   
}
