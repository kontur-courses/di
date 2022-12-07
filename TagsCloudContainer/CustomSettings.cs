using System.Drawing;

namespace TagsCloudContainer
{
    public class CustomSettings
    {
        public string FontName { get; }
        public string BackGroundColor { get; }
        public string BrushColor { get; }

        public string PathToSave { get; }
        public string PathToOpen { get; }
        public int CanvasWidth { get; }
        public int CanvasHeight { get; }

        public int CanvasBorder { get; }

        public double RadiusOffset { get; }
        public double AngleOffset { get; }

        public CustomSettings(string font,
            string backGroundColor,
            string brushColor,
            string pathToSave,
            string pathToOpen,
            int canvasWidth,
            int canvasHeight,
            int canvasBorder,
            double radiusOffset,
            double angleOffset)
        {
            FontName = font;
            BackGroundColor = backGroundColor;
            BrushColor = brushColor;
            PathToSave = pathToSave;
            PathToOpen = pathToOpen;
            CanvasWidth = canvasWidth;
            CanvasHeight = canvasHeight;
            CanvasBorder = canvasBorder;
            RadiusOffset = radiusOffset;
            AngleOffset = angleOffset;
        }
    }
}