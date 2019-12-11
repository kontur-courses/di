using System.Drawing;

namespace TagsCloudContainer.Visualization.Painters
{
    public class ColorizedRectangle
    {
        internal readonly Rectangle Rectangle;
        internal readonly Brush FillBrush;
        internal readonly Pen BorderPen;

        internal ColorizedRectangle(Rectangle rectangle, Brush fillBrush, Pen borderPen)
        {
            Rectangle = rectangle;
            FillBrush = fillBrush;
            BorderPen = borderPen;
        }
    }
}