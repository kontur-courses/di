using System.Drawing;

namespace TagsCloudContainerCore
{
    public class TagToRender
    {
        private Graphics graphics;
        public readonly string Tag;
        public Point Location { get;  set; }

        public Size TagSize => graphics.MeasureString(Tag, Font).ToSize();

        public TagToRender(string tag, Graphics graphics, Font font)
        {
            Tag = tag;
            this.graphics = graphics;
        }


        public Font Font { get; set; }
    }
}