using System.Drawing;

namespace TagsCloudContainer.Visualizing
{
    public interface IColorHandler
    {
        Color GetColorFor(string word, Rectangle rectangle);
    }
}