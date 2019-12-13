using TagsCloudVisualization.Core;
using TagsCloudVisualization.Layouters;

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