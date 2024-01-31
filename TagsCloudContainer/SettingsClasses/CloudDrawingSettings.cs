using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer.SettingsClasses
{
    public class CloudDrawingSettings
    {
        public FontFamily FontFamily = new("Arial");
        public float FontSize = 12;
        public IList<Color> Colors = new List<Color>() { Color.AliceBlue };
        public Color BgColor = Color.Black;
        public Size Size = new(600, 600);
        public IPointsProvider PointsProvider = new SpiralPointsProvider();
    }
}