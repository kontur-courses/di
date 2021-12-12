using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer
{
    public class TagsCloudDirector : ITagsCloudDirector
    {
        private readonly IEnumerable<IWordsPreprocessor> preprocessors;
        private readonly IWordColorMapperSettings colorMapperSettings;
        private readonly ITagsCloudLayouter tagsCloudLayouter;
        private readonly ITagsCloudRenderer renderer;

        public TagsCloudDirector(
            IEnumerable<IWordsPreprocessor> preprocessors,
            IWordColorMapperSettings colorMapperSettings,
            ITagsCloudRenderer renderer,
            ITagsCloudLayouter tagsCloudLayouter)
        {
            this.preprocessors = preprocessors;
            this.colorMapperSettings = colorMapperSettings;
            this.renderer = renderer;
            this.tagsCloudLayouter = tagsCloudLayouter;
        }

        public Bitmap RenderWords(IEnumerable<string> words)
        {
            var preprocessedWords = PreprocessWords(words);
            var layout = tagsCloudLayouter.GetCloudLayout(preprocessedWords);
            var colorMap = colorMapperSettings.ColorMapper.GetColorMap(layout);
            var wordsStyles = GetWordsStyles(layout, colorMap).ToList();
            var bitmap = renderer.GetBitmap(wordsStyles, layout.ImageSize);
            wordsStyles.ForEach(x => x.Dispose());
            return bitmap;
        }

        private IEnumerable<string> PreprocessWords(IEnumerable<string> words)
        {
            foreach (var preprocessor in preprocessors)
                words = preprocessor.Preprocess(words);

            return words;
        }

        private static IEnumerable<WordStyle> GetWordsStyles(
            CloudLayout layout,
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
            renderer.Dispose();
        }
    }
}