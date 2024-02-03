using System.Drawing;
namespace TagCloud;

public interface IColorGenerator
{
    Color GetNextColor();
}