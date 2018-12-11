using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Filters;
using TagsCloudContainer.Formatters;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Visualisation;
using TagsCloudContainer.Weighting;

namespace TagsCloudContainer
{
    public class TagsCloudGenerator
    {
        private readonly Size minLetterSize;
        private readonly IWordsFormatter wordsFormatter;
        private readonly IWordsFilter wordsFilter;
        private readonly ITagsCloudLayouter layouter;
        private readonly IWordsWeighter wordsWeighter;

        public TagsCloudGenerator(TagsCloudGeneratorSettings settings)
        {
            minLetterSize = settings.LetterSize;
            wordsFilter = settings.WordsFilter;
            wordsFormatter = settings.WordsFormatter;
            layouter = settings.TagsCloudLayouter;
            wordsWeighter = settings.WordsWeighter;
        }

        public ITagsCloud CreateCloud(List<string> words)
        {
            words = wordsFormatter.Format(words);
            words = wordsFilter.Filter(words).ToList();
            var wordsSizes = wordsWeighter.GetWordsSizes(words, minLetterSize);

            foreach (var pair in wordsSizes)
            {
                var rectangle = layouter.PutNextRectangle(pair.Value);
                layouter.TagsCloud.AddWord(new TagsCloudWord(pair.Key, rectangle));
            }

            return layouter.TagsCloud;
        }
    }
}