using System.Drawing;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class TagsCloudGeneratorSettings
    {
        public Size LetterSize;
        public IWordsFormatter WordsFormatter;
        public IWordsFilter WordsFilter;
        public ITagsCloudLayouter TagsCloudLayouter;
        public IWordsWeighter WordsWeighter;

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