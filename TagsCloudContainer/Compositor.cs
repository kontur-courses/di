using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Infrastructure.Common;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class Compositor
    {
        private IWordsSelector wordSelector;
        private ICloudLayouter layouter;
        private AlgorithmsSettings settings;

        public Compositor(IWordsSelector wordSelector, ICloudLayouter layouter, AlgorithmsSettings settings)
        {
            this.wordSelector = wordSelector;
            this.layouter = layouter;
            this.settings = settings;
        }

        public HashSet<(Rectangle, LayoutWord)> Composite()
        {
            var words = new HashSet<(Rectangle, LayoutWord)>();
            var layoutWords = wordSelector.Select();
            if (settings.Centering)
                layoutWords = layoutWords.OrderBy(x => -x.Size.Width * x.Size.Height);
            foreach (var layoutWord in layoutWords)
            {
                var rectangle = layouter.PutNextRectangle(layoutWord.Size);
                words.Add((rectangle, layoutWord));
            }

            return words;
        }
    }
}