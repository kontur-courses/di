using System.Drawing;

namespace TagsCloudContainer.Visualization.Painters
{
    public class ColoringScheme
    {
        internal readonly Brush TextBrush;
        internal readonly Brush RectangleFillBrush;
        internal readonly Pen RectangleBorderPen;

        internal ColoringScheme(Brush textBrush, Brush rectangleFillBrush, Pen rectangleBorderPen)
        {
            TextBrush = textBrush;
            RectangleFillBrush = rectangleFillBrush;
            RectangleBorderPen = rectangleBorderPen;
        }
    }
}