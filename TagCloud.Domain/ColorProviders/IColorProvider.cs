using System.Drawing;

public interface IColorProvider
{
    Color GetColor(WordLayout layout);
}
