using System.Collections.Generic;
using System.Linq;
using TagCloud.Algorithm;
using TagCloud.App;
using TagCloud.Infrastructure;
using TagCloud.Visualization.WordPainting;

namespace TagCloud.Visualization
{
    public class TagCloudElementsPreparer : ITagCloudElementsPreparer
    {
        private readonly ITagCloudLayouter tagCloudLayouter;
        private readonly IWordPainter wordPainter;
      //  private readonly IWordPainterProvider wordPainterProvider;
        private readonly PictureConfig config;

        public TagCloudElementsPreparer(
            ITagCloudLayouter tagCloudLayouter, 
            IWordPainter wordPainter,
            //IWordPainterProvider wordPainterProvider, 
            PictureConfig config)
        {
            this.tagCloudLayouter = tagCloudLayouter;
            this.wordPainter = wordPainter;
           // this.wordPainterProvider = wordPainterProvider;
            this.config = config;
        }

        public IEnumerable<TagCloudElement> PrepareTagCloudElements(IEnumerable<Word> words)
        {
            foreach (var pair in words.Select((w, i) => new { Word = w, Index = i }))
            {
                var word = pair.Word;
                var index = pair.Index;
                var rectangle = tagCloudLayouter.PutNextRectangle(word.WordRectangleSize);
                var color = wordPainter.GetWordColor(word, index);
                yield return new TagCloudElement(word, rectangle, color, config.FontFamily);
            }
        }
    }
}