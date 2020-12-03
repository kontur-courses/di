using System.Drawing;

namespace TagsCloudVisualization.Configs
{
    public interface IConfig
    {
        Font Font { get; }
        Point Center { get; }

        Color TextColor { get; }

        Size ImageSize { get; }

        public void SetValues(Font font, Point center, Color textColor, Size size);
    }
}