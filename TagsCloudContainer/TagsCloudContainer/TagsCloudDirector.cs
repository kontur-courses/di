using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.WordsLoading;

namespace TagsCloudContainer
{
    public class TagsCloudDirector
    {
        private readonly IWordsProvider wordsProvider;
        private readonly IWordsFilter[] wordsFilters;
        private readonly ITagsCloudLayouter tagsCloudLayouter;
        private readonly IWordColorMapper wordColorMapper;
        private readonly ITagsCloudRenderer tagsCloudRenderer;

        public TagsCloudDirector(IWordsProvider wordsProvider, IWordsFilter[] wordsFilters,
            ITagsCloudLayouter tagsCloudLayouter,
            IWordColorMapper wordColorMapper, ITagsCloudRenderer tagsCloudRenderer)
        {
            this.wordsProvider = wordsProvider;
            this.wordsFilters = wordsFilters;
            this.tagsCloudLayouter = tagsCloudLayouter;
            this.wordColorMapper = wordColorMapper;
            this.tagsCloudRenderer = tagsCloudRenderer;
        }

        public void Render()
        {
            var words = wordsProvider.GetWords().Select(word => word.ToLower());
            foreach (var wordsFilter in wordsFilters)
                words = wordsFilter.Filter(words);

            var layout = tagsCloudLayouter.GetCloudLayout(words);
            var colorMap = wordColorMapper.GetColorMap(layout);

            var wordsStyles = new List<WordStyle>();
            foreach (var wordLayout in layout.WordLayouts)
            {
                var brush = new SolidBrush(colorMap[wordLayout]);
                wordsStyles.Add(new WordStyle(wordLayout.Word, wordLayout.Font, wordLayout.Location, brush));
            }

            tagsCloudRenderer.Render(wordsStyles, layout.ImageSize);
        }
    }
}