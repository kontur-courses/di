using System.Collections.Generic;
using System.Drawing;
using TagsCloudGenerator.CloudLayouter;
using TagsCloudGenerator.WordsHandler;

namespace TagsCloudGenerator
{
    public class CloudGenerator : ICloudGenerator
    {
        private readonly IWordHandler handler;
        private readonly ICloudLayouter layouter;

        public CloudGenerator(IWordHandler handler, ICloudLayouter layouter)
        {
            this.handler = handler;
            this.layouter = layouter;
        }

        public Cloud Generate(Dictionary<string, int> wordsToCount, Font font)
        {
            var validWords = handler.GetValidWords(wordsToCount);

            return layouter.LayoutWords(validWords, font);
        }
    }
}