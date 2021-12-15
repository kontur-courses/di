using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public interface IColorScheme
    {
        Brush GetNextColor();
    }
}
