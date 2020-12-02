using System.Drawing;

namespace TagsCloudVisualization
{
    public class Config : IConfig
    {
        public Font Font { get; private set; }
        public Point Center { get; private set; }

        public Color TextColor { get; private set; }

        public void SetValues(Font font, Point center, Color textColor)
        {
            Font = font;
            Center = center;
            TextColor = textColor;
        }
    }
}