using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsLoading;

namespace TagsCloudContainer
{
    public class TagsCloudDirector
    {
        private readonly IFileLoaderFactory fileLoaderFactory;
        private readonly IFileLoadSettings fileLoadSettings;
        private readonly IWordsFiltersSettings wordsFiltersSettings;
        private readonly ITagsCloudLayouterSettings defaultTagsCloudLayouterSettings;
        private readonly IWordsColorSettings wordsColorSettings;
        private readonly ITagsCloudImageSaver tagsCloudImageSaver;

        public TagsCloudDirector(
            IFileLoaderFactory fileLoaderFactory,
            IFileLoadSettings fileLoadSettings,
            IWordsFiltersSettings wordsFiltersSettings,
            ITagsCloudLayouterSettings defaultTagsCloudLayouterSettings,
            IWordsColorSettings wordsColorSettings,
            ITagsCloudImageSaver tagsCloudImageSaver)
        {
            this.fileLoaderFactory = fileLoaderFactory;
            this.fileLoadSettings = fileLoadSettings;
            this.wordsFiltersSettings = wordsFiltersSettings;
            this.defaultTagsCloudLayouterSettings = defaultTagsCloudLayouterSettings;
            this.wordsColorSettings = wordsColorSettings;
            this.tagsCloudImageSaver = tagsCloudImageSaver;
        }

        public void Render()
        {
            var fileLoader = fileLoaderFactory.GetByFileName(fileLoadSettings.FileName);
            var words = fileLoader.LoadWords(fileLoadSettings.FileName).Select(word => word.ToLower());
            foreach (var wordsFilter in wordsFiltersSettings.Filters)
                words = wordsFilter.Filter(words);

            var layout = defaultTagsCloudLayouterSettings.Layouter.GetCloudLayout(words);
            var colorMap = wordsColorSettings.ColorMapper.GetColorMap(layout);

            var wordsStyles = new List<WordStyle>();
            foreach (var wordLayout in layout.WordLayouts)
            {
                var brush = new SolidBrush(colorMap[wordLayout]);
                wordsStyles.Add(new WordStyle(wordLayout.Word, wordLayout.Font, wordLayout.Location, brush));
            }

            tagsCloudImageSaver.Save(wordsStyles, layout.ImageSize);
        }
    }
}