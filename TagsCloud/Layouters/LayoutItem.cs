using System.Drawing;

namespace TagsCloud.Layouters
{
    public class LayoutItem
    {
        public Rectangle Rectangle;
        public string Text;
        public int Rate;

        public LayoutItem(Rectangle rectangle, string text, int rate)
        {
            Rectangle = rectangle;
            Text = text;
            Rate = rate;
        }
    }
}
