using System.Drawing;
using TagsCloudContainer.Visualization.Measurers;

namespace TagsCloudContainer.Visualization.Painters
{
    public class ColorizedTag : ColorizedRectangle
    {
        internal readonly Tag Tag;
        internal readonly Brush TextBrush;

        internal ColorizedTag(Tag tag, Brush textBrush, Brush fillBrush, Pen borderPen) :
            base(tag.Rectangle, fillBrush, borderPen)
        {
            Tag = tag;
            TextBrush = textBrush;
        }
    }
}