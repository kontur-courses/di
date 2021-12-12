using System.Collections.Generic;
using TagsCloud.Visualization.FontFactory;
using TagsCloud.Visualization.Models;
using TagsCloud.Visualization.WordsSizeServices;

namespace TagsCloud.Visualization.LayoutContainer.ContainerBuilder
{
    public class WordsContainerBuilder : AbstractWordsContainerBuilder
    {
        private readonly IFontFactory fontFactory;
        private readonly ICloudLayouter layouter;
        private readonly List<WordWithBorder> words = new();
        private readonly IWordsSizeService wordsSizeService;

        public WordsContainerBuilder(
            ICloudLayouter layouter,
            IWordsSizeService wordsSizeService,
            IFontFactory fontFactory)
        {
            this.layouter = layouter;
            this.wordsSizeService = wordsSizeService;
            this.fontFactory = fontFactory;
        }

        protected override WordsContainerBuilder AddWord(Word word, int minCount, int maxCount)
        {
            var font = fontFactory.GetFont(word, minCount, maxCount);
            var size = wordsSizeService.CalculateSize(word, font);
            var rectangle = layouter.PutNextRectangle(size);
            words.Add(new WordWithBorder(word, font, rectangle));
            return this;
        }

        public override WordsContainer Build() => new() {Items = words};
    }
}