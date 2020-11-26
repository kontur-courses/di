using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualisation.Layouter;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public sealed class WordsLayouterAsset
    {
        private readonly ICircularCloudLayouter layouter;
        private readonly WordsVisualiser wordsVisualiser;
        private readonly int scale;
        private readonly List<(WordToDraw toDraw, Rectangle position)> words = new List<(WordToDraw, Rectangle)>();

        public WordsLayouterAsset(ICircularCloudLayouter layouter, int scale)
        {
            this.layouter = layouter;
            wordsVisualiser = new WordsVisualiser(layouter.CloudCenter, 1);
            this.scale = scale;
        }

        public void DrawWord(WordToDraw toDraw)
        {
            var position = PutNextWord(ref toDraw);
            wordsVisualiser.Draw(position, toDraw);
        }

        public void EnqueueWord(WordToDraw toDraw)
        {
            var position = PutNextWord(ref toDraw);
            words.Add((toDraw, position));
        }

        public void DrawQueuedWords()
        {
            foreach (var (toDraw, position) in words)
                wordsVisualiser.Draw(position, toDraw);
            words.Clear();
        }

        public Image GetImage() => wordsVisualiser.GetImage();

        private Rectangle PutNextWord(ref WordToDraw toDraw)
        {
            toDraw = new WordToDraw(toDraw.Word, WordToDraw.MultiplyFontSize(toDraw.Font, scale), toDraw.Brush);
            var wordSize = wordsVisualiser.MeasureString(toDraw);
            return layouter.PutNextRectangle(Size.Ceiling(wordSize));
        }
    }
}