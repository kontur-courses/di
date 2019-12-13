using System.Drawing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class VisualizationSettings
    {
        public Font Font { get; }
        public Brush BackgroundBrush { get; }
        public Brush TextBrush { get; }
        public bool IsDebugMode { get; }
        public Color DebugMarkingColor { get; }
        public Color DebugItemRectangleColor { get; }

        public VisualizationSettings(Font font, Brush backgroundBrush, Brush textBrush, bool isDebugMode,
            Color debugMarkingColor, Color debugItemRectangleColor)
        {
            Font = font;
            BackgroundBrush = backgroundBrush;
            TextBrush = textBrush;
            IsDebugMode = isDebugMode;
            DebugMarkingColor = debugMarkingColor;
            DebugItemRectangleColor = debugItemRectangleColor;
        }
    }
}