using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.Settings;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Painter;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.UI.Actions
{
    public class CloudDrawAction : IUiAction
    {
        private readonly IFileReader reader;
        private readonly WordsPreprocessor preprocessor;
        private readonly WordsPreprocessorSettings preprocessorSettings;
        private readonly IFilePathProvider filePath;
        private readonly TagCloudPainter painter;
        private readonly LayouterApplicator infoGetter;

        public CloudDrawAction(IFileReader reader,
            WordsPreprocessor preprocessor,
            WordsPreprocessorSettings preprocessorSettings,
            IFilePathProvider filePath,
            TagCloudPainter painter,
            LayouterApplicator infoGetter)
        {
            this.reader = reader;
            this.preprocessor = preprocessor;
            this.preprocessorSettings = preprocessorSettings;
            this.filePath = filePath;
            this.painter = painter;
            this.infoGetter = infoGetter;
        }

        public MenuCategory Category => MenuCategory.TagCloud;
        public string Name => "Cгенерировать облако";
        public string Description => "Сгенерировать облако из файла";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Path.GetFullPath(filePath.WordsFilePath),
                DefaultExt = "txt",
                Filter = "Файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK)
                return;
            filePath.WordsFilePath = dialog.FileName;
            var words = reader.Read();
            res = SettingsForm.For(preprocessorSettings).ShowDialog();
            if (res != DialogResult.OK)
                return;
            var processed = preprocessor.CountWordFrequencies(words);
            var wordInfos = infoGetter.GetWordsAndRectangles(processed);
            painter.Paint(infoGetter.WordsCenter, wordInfos);
        }
    }
}