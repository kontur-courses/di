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
                var rectangle = layouter.PutNextRectangle(layoutWord.Size);
                words.Add((rectangle, layoutWord));
            }

            return words;
        }
    }
}