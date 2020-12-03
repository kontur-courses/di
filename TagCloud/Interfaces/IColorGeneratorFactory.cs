using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public interface IColorGeneratorFactory
    {
        IColorGenerator Get(Color[] colors);
    }
}