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
    public class CloudLayouterAction : IFormAction
    {
        public string Category => "Tag cloud";
        public string Name => "Build tag cloud";
        public string Description => "Build your tag cloud from text";

        private readonly ITagCloudBuilder tagCloudBuilder;
        private readonly ICloudVisualizer cloudVisualizer;
        private readonly SpiralParams spiralParams;
        private readonly ForbiddenWordsSettings forbiddenWordsSettings;

        public CloudLayouterAction(
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
            var dialog = new OpenFileDialog
            {
                Title = "Select file to build a tag cloud",
                CheckFileExists = false,
                Multiselect = false,
                DefaultExt = "txt",
                InitialDirectory = @"C:\Users\Public\Documents",
                Filter = "Текстовый документ |*.txt;*.doc;*.docx"
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK)
                return;
            
            SettingsForm.For(spiralParams).ShowDialog();
            
            var text = TextReader.ReadAllText(dialog.FileName);
            var wordsFrequency =
                TextHandler.GetOrderedByFrequencyWords(text, forbiddenWordsSettings.ForbiddenWords.ToHashSet());
            var tagCloud = tagCloudBuilder.Build(wordsFrequency);
            cloudVisualizer.PrintTagCloud(tagCloud);
        }
    }
}