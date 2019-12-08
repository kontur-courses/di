using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class Compositor
    {
        private IWordsSelector wordSelector;
        private ICloudLayouter layouter;

        public Compositor(IWordsSelector wordSelector, ICloudLayouter layouter)
        {
            this.wordSelector = wordSelector;
            this.layouter = layouter;
        }

        public HashSet<(Rectangle, LayoutWord)> Composite()
        {
            var words = new HashSet<(Rectangle, LayoutWord)>();
            foreach (var layoutWord in wordSelector.Select())
            {
                var size = new Size(layoutWord.Word.Length * layoutWord.Count,
                    layoutWord.Font.Height * layoutWord.Count);
                var rectangle = layouter.PutNextRectangle(size);
                words.Add((rectangle, layoutWord));
            }

            return words;
        }
    }
}