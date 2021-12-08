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
        private readonly IFileTextLoaderFactory fileTextLoaderFactory;
        private readonly IWordsParser wordsParser;
        private readonly IEnumerable<IWordsPreprocessor> preprocessors;
        private readonly IFileLoadSettings fileLoadSettings;
        private readonly IWordsColorSettings wordsColorSettings;
        private readonly ITagsCloudImageSaver tagsCloudImageSaver;
        private readonly ITagsCloudLayouter tagsCloudLayouter;

        public TagsCloudDirector(
            IFileTextLoaderFactory fileTextLoaderFactory,
            IWordsParser wordsParser,
            IEnumerable<IWordsPreprocessor> preprocessors,
            IFileLoadSettings fileLoadSettings,
            IWordsColorSettings wordsColorSettings,
            ITagsCloudImageSaver tagsCloudImageSaver,
            ITagsCloudLayouter tagsCloudLayouter)
        {
            this.fileTextLoaderFactory = fileTextLoaderFactory;
            this.wordsParser = wordsParser;
            this.preprocessors = preprocessors;
            this.fileLoadSettings = fileLoadSettings;
            this.wordsColorSettings = wordsColorSettings;
            this.tagsCloudImageSaver = tagsCloudImageSaver;
            this.tagsCloudLayouter = tagsCloudLayouter;
        }

        public void Render()
        {
            var words = GetWords();
            var layout = tagsCloudLayouter.GetCloudLayout(words);
            var colorMap = wordsColorSettings.ColorMapper.GetColorMap(layout);
            var wordsStyles = GetWordsStyles(layout, colorMap);
            tagsCloudImageSaver.Save(wordsStyles, layout.ImageSize);
        }

        private IEnumerable<string> GetWords()
        {
            var fileLoader = fileTextLoaderFactory.GetByFileName(fileLoadSettings.FileName);
            var fileText = fileLoader.LoadText(fileLoadSettings.FileName);
            var words = wordsParser.Parse(fileText);
            foreach (var preprocessor in preprocessors)
                words = preprocessor.Preprocess(words);

            return words;
        }

        private static IEnumerable<WordStyle> GetWordsStyles(CloudLayout layout,
            IReadOnlyDictionary<WordLayout, Color> colorMap)
        {
            foreach (var wordLayout in layout.WordLayouts)
            {
                var brush = new SolidBrush(colorMap[wordLayout]);
                yield return new WordStyle(wordLayout.Word, wordLayout.Font, wordLayout.Location, brush);
            }
        }

        public void Dispose()
        {
            tagsCloudImageSaver.Dispose();
        }
    }
}