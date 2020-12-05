using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.AppSettings;
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

        private readonly ITagCloudBuilder tagCloudBuilder;
        private readonly ICloudVisualizer cloudVisualizer;
        private readonly SpiralParams spiralParams;
        private readonly ForbiddenWordsSettings forbiddenWordsSettings;

        public ExampleAction(
            ITagCloudBuilder tagCloudBuilder, ICloudVisualizer cloudVisualizer, SpiralParams spiralParams,
            ForbiddenWordsSettings forbiddenWordsSettings)
        {
            this.tagCloudBuilder = tagCloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.spiralParams = spiralParams;
            this.forbiddenWordsSettings = forbiddenWordsSettings;
        }

        public void Perform()
        {
            var result = SettingsForm.For(spiralParams).ShowDialog();
            if (result != DialogResult.OK)
                return;
            
            var text = TextReader.ReadAllText(@"..\..\..\Examples\example.txt");
            var wordsFrequency =
                TextHandler.GetOrderedByFrequencyWords(text, forbiddenWordsSettings.ForbiddenWords.ToHashSet());
            var tagCloud = tagCloudBuilder.Build(wordsFrequency);
            cloudVisualizer.PrintTagCloud(tagCloud);
        }
    }
}