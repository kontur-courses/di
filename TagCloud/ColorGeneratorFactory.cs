using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class ColorGeneratorFactory : IColorGeneratorFactory
    {
        public IColorGenerator Get(Color[] colors)
        {
            return new ColorGenerator(colors);
        }
    }
}