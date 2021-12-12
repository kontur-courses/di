using System.Collections.Generic;
using System.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.Drawing
{
    public interface IDrawer
    {
        Bitmap Draw(IDrawerOptions options, List<Word> words);
    }
}