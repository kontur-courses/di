using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Painters
{
    public abstract class WordPainter
    {
        protected readonly Palette palette;

        public WordPainter(Palette palette)
        {
            this.palette = palette;
        }

        public abstract PaintedWord[] GetPaintedWords(AnalyzedLayoutedText analyzedLayoutedText);
    }
}