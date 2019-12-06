using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public class ColoringOptions
    {
        public readonly Pen rectangleBorderPen;
        public readonly Brush rectangleFillBrush;
        public readonly Brush backgroundFillBrush;
        public readonly Brush textBrush;

        public ColoringOptions(
            Brush rectangleFillBrush,
            Brush backgroundFillBrush,
            Pen rectangleBorderPen,
            Brush textBrush)
        {
            this.rectangleFillBrush = rectangleFillBrush;
            this.backgroundFillBrush = backgroundFillBrush;
            this.rectangleBorderPen = rectangleBorderPen;
            this.textBrush = textBrush;
        }
    }
}