using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings.Interfaces;
using TagsCloudContainer.WordsLoading;

namespace TagsCloudContainer
{
    public class TagsCloudDirector : ITagsCloudDirector
    {
        private readonly IFileLoaderFactory fileLoaderFactory;
        private readonly IEnumerable<IWordsPreprocessor> preprocessors;
        private readonly IFileLoadSettings fileLoadSettings;
        private readonly IWordsColorSettings wordsColorSettings;
        private readonly ITagsCloudImageSaver tagsCloudImageSaver;
        private readonly ITagsCloudLayouter tagsCloudLayouter;

        public TagsCloudDirector(
            IFileLoaderFactory fileLoaderFactory,
            IEnumerable<IWordsPreprocessor> preprocessors,
            IFileLoadSettings fileLoadSettings,
            IWordsColorSettings wordsColorSettings,
            ITagsCloudImageSaver tagsCloudImageSaver,
            ITagsCloudLayouter tagsCloudLayouter)
        {
            this.fileLoaderFactory = fileLoaderFactory;
            this.preprocessors = preprocessors;
            this.fileLoadSettings = fileLoadSettings;
            this.wordsColorSettings = wordsColorSettings;
            this.tagsCloudImageSaver = tagsCloudImageSaver;
            this.tagsCloudLayouter = tagsCloudLayouter;
        }

        public void Render()
        {
            var fileLoader = fileLoaderFactory.GetByFileName(fileLoadSettings.FileName);
            var words = fileLoader.LoadWords(fileLoadSettings.FileName);
            foreach (var preprocessor in preprocessors)
                words = preprocessor.Preprocess(words);

            var layout = tagsCloudLayouter.GetCloudLayout(words);
            var colorMap = wordsColorSettings.ColorMapper.GetColorMap(layout);

            var wordsStyles = new List<WordStyle>();
            foreach (var wordLayout in layout.WordLayouts)
            {
                var brush = new SolidBrush(colorMap[wordLayout]);
                wordsStyles.Add(new WordStyle(wordLayout.Word, wordLayout.Font, wordLayout.Location, brush));
            }

            tagsCloudImageSaver.Save(wordsStyles, layout.ImageSize);
        }

        public void Dispose()
        {
            tagsCloudImageSaver?.Dispose();
        }
    }
}