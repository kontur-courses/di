using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class ArialFontScheme : IFontScheme
    {
        public Font Process(PositionedElement element)
            => new Font("Arial", (element.Frequency + 1) * 3);
    }
}