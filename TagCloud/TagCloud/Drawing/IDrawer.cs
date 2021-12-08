using System.Collections.Generic;
using System.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.Drawing
{
    internal interface IDrawer
    {
        Bitmap Draw(IDrawerOptions options, List<Word> words);
    }
}