using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagCloudDrawer
    {
        Bitmap Draw(List<Word> words);
    }
}