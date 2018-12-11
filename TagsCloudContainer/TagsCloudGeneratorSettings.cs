using System.Drawing;
using TagsCloudContainer.Filters;
using TagsCloudContainer.Formatters;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualisation;
using TagsCloudContainer.Weighting;

namespace TagsCloudContainer
{
    public class TagsCloudGeneratorSettings
    {
        public Size LetterSize;
        public readonly IWordsFormatter WordsFormatter;
        public readonly IWordsFilter WordsFilter;
        public readonly ITagsCloudLayouter TagsCloudLayouter;
        public readonly IWordsWeighter WordsWeighter;

        public TagsCloudGeneratorSettings
        (Size letterSize, IWordsFormatter wordsFormatter, IWordsFilter wordsFilter, ITagsCloudLayouter layouter,
            IWordsWeighter weighter)
        {
            LetterSize = letterSize;
            WordsFormatter = wordsFormatter;
            WordsFilter = wordsFilter;
            TagsCloudLayouter = layouter;
            WordsWeighter = weighter;
        }

        public TagsCloudGeneratorSettings
            (IUI ui, IWordsFormatter wordsFormatter, IWordsFilter wordsFilter, ITagsCloudLayouter layouter,IWordsWeighter weighter)
        {
            LetterSize = ui.LetterSize;
            WordsFormatter = wordsFormatter;
            WordsFilter = wordsFilter;
            TagsCloudLayouter = layouter;
            WordsWeighter = weighter;
        }
    }
}