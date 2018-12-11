using System.Drawing;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;

namespace TagCloud
{
    public class RedColorScheme : IColorScheme
    {
        public const int MaxChannelValue = 255;
        public const int MaxChannelDelta = 200;

        public Color Process(PositionedElement element)
        {
            return Color.FromArgb(MaxChannelValue - MaxChannelDelta / element.Frequency, Color.Red);
        }
    }
}