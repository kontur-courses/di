using System.Collections.Generic;
using TagCloud.Algorithm;
using TagCloud.Infrastructure;
using TagCloud.Visualization.WordPainting;

namespace TagCloud.Visualization
{
    public class TagCloudElementsPreparer : ITagCloudElementsPreparer
    {
        public int CurrentWordIndex { get; private set; }

        private readonly ITagCloudLayouter tagCloudLayouter;
        private readonly IWordPainter wordPainter;
        private readonly PictureConfig config;

        public TagCloudElementsPreparer(
            ITagCloudLayouter tagCloudLayouter, 
            IWordPainter wordPainter,
            PictureConfig config)
        {
            this.tagCloudLayouter = tagCloudLayouter;
            this.wordPainter = wordPainter;
            this.config = config;
        }

        public IEnumerable<TagCloudElement> PrepareTagCloudElements(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                var rectangle = tagCloudLayouter.PutNextRectangle(word.WordRectangleSize);
                var color = wordPainter.GetWordColor(word);
                yield return new TagCloudElement(word, rectangle, color, config.FontFamily);
                CurrentWordIndex++;
            }
        }

        
    }
}