using System.Collections.Generic;
using System.Drawing;
using TagCloud.PreLayout;

namespace TagCloud.Drawing
{
    public interface IDrawer
    {
        Bitmap Draw(IDrawerOptions options, List<Word> words);
    }
}