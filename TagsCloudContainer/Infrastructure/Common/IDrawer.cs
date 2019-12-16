using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Infrastructure.Common
{
    public interface IDrawer
    {
        Bitmap Draw(ImageSetting setting, IEnumerable<(Rectangle, LayoutWord)> words);
    }
}