using System.Drawing;
using TagCloud.Interfaces;
using TagCloud.IntermediateClasses;

namespace TagCloud
{
    public class RedColorScheme : IColorScheme
    {
        public Color Process(PositionedElement element)
        {
            return Color.FromArgb(element.Frequency * 10, Color.Red);
        }
    }
}