using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloudBuilders;
using TagsCloudVisualization.TagCloudVisualizers;
using TagsCloudVisualization.TextProcessing.TextHandler;
using TagsCloudVisualization.TextProcessing.TextReader;

namespace TagsCloudVisualization.FormAction
{
    public class ExampleAction : IFormAction
    {
        public string Category => "Tag cloud";
        public string Name => "Build example";
        public string Description => "Build example tag cloud";
        
        private ITagCloudBuilder tagCloudBuilder;
        private ICloudVisualizer cloudVisualizer;
        private SpiralParams spiralParams;
        

        public ExampleAction(
            ITagCloudBuilder tagCloudBuilder, ICloudVisualizer cloudVisualizer, SpiralParams spiralParams)
        {
            this.tagCloudBuilder = tagCloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.spiralParams = spiralParams;
        }
        
        public void Perform()
        {
            var text = TextReader.ReadAllText(@"..\..\..\Examples\example.txt");
            var wordsFrequency = TextHandler.GetOrderedByFrequencyWords(text);
            var tagCloud = tagCloudBuilder.Build(wordsFrequency);
            cloudVisualizer.PrintTagCloud(tagCloud);
        }
    }
}