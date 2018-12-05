using System.Collections.Generic;
using System.Drawing;
using TagCloud.Util;

namespace TagCloud.Painters
{
    public class OneColorPainter : IPainter
    {
        private readonly Brush tagColor;
        public Color BackgroundColor { get; }

        public OneColorPainter(Color backgroundColor, Brush tagColor)
        {
            this.tagColor = tagColor;
            BackgroundColor = backgroundColor;
        }

        public void PaintTags(List<Tag> tags)
        {
            foreach (var tag in tags)
                tag.Brush = tagColor;
        }

        public void SetBackgroundColorFor(Graphics graphics)
        {
            graphics.Clear(BackgroundColor);
        }
    }
}