using System.Drawing;
namespace TagCloud;

public interface IColorGeneratorFactory
{
    IColorGenerator Get(Color[] colors);
}