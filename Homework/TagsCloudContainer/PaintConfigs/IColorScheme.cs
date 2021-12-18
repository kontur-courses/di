using System;
using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public interface IColorScheme : IDisposable
    {
        Brush GetNextColor();
    }
}
