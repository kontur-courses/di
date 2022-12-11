using System.Drawing;
using TagCloud.CloudLayouters;
using TagCloud.TagCloudCreators;
using TagCloud.Tags;
using TagCloud.WordPreprocessors;

namespace TagCloud.TagCloudVisualizations
{
    public class WordTagCloudBitmapVisualization : TagCloudBitmapVisualization
    {
        public WordTagCloudBitmapVisualization(
            ICloudLayouter.Factory cloudLayouterFactory, 
            IWordPreprocessor wordPreprocessor, 
            ITagCloudCreator.Factory tagCloudCreatorFactory) : 
            base(cloudLayouterFactory, wordPreprocessor, tagCloudCreatorFactory)
        {
        }

        public override void DrawIn(Graphics graphics, ITag tag, Brush byBrush)
        {
            var word = tag as Word;
            graphics.DrawString(word.Text, word.Font, byBrush, word.Frame.Location);
        }
    }
}
