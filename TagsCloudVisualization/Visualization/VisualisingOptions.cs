using System.Drawing;

namespace TagsCloudVisualization.Visualization
{
    public class VisualisingOptions
    {
        public readonly Font Font;
        public readonly Size ImageSize;
        public readonly Color BackgroundColor;
        public readonly Color TextColor;

        public VisualisingOptions(Font font, Size imageSize, Color backgroundColor, Color textColor)
        {
            Font = font;
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }

        public static VisualisingOptions GetDefaultOptions()
        {
            return new VisualisingOptions(new Font("Arial", 30),
                new Size(1000, 1000), Color.Black, Color.White);
        }
    }
}