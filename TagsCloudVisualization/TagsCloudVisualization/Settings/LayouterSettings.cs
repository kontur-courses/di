using System.Drawing;
using TagsCloudVisualization.Providers.Layouter.Interfaces;

namespace TagsCloudVisualization.Settings
{
    public class LayouterSettings
    {
        public LayouterSettings(Point center, int spiralCoefficient, SpiralType spiralType)
        {
            Center = center;
            SpiralCoefficient = spiralCoefficient;
            SpiralType = spiralType;
        }

        public Point Center { get; }
        public int SpiralCoefficient { get; }
        public SpiralType SpiralType { get; }
    }
}