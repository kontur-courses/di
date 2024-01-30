using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer.SettingsClasses
{
    public class CloudDrawingSettings
    {
        public FontFamily FontFamily = new FontFamily("Arial");
        public float FontSize = 12;
        public IList<Color> Colors = new List<Color>() { Color.AliceBlue };
        public Size Size = new Size(600, 600);
        public IPointsProvider PointsProvider = new SpiralPointsProvider();
    }
}