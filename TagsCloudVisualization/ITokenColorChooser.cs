using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITokenColorChooser
    {
        Color GetTokenColor(Token token);
    }
}