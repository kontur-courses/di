using System;
using System.Drawing;

namespace TagsCloudVisualization.Painters
{
    public interface IWordLayoutPainter
    {
        Bitmap GetDrawnLayoutedWords(LayoutedWord[] layoutedWords);
    }
}