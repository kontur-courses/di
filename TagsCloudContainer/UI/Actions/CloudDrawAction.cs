using System.IO;
using System.Linq;
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
        private readonly IWordsPreprocessor[] preprocessors;
        private readonly FrequencyCounter frequencyCounter;
        private readonly WordsPreprocessorSettings preprocessorSettings;
        private readonly IFilePathProvider filePath;
        private readonly TagCloudPainter painter;
        private readonly LayouterApplicator applicator;
        private readonly PictureBoxImageHolder imageHolder;

        public CloudDrawAction(IFileReader reader,
            IWordsPreprocessor[] preprocessors,
            FrequencyCounter frequencyCounter,
            WordsPreprocessorSettings preprocessorSettings,
            IFilePathProvider filePath,
            TagCloudPainter painter,
            LayouterApplicator applicator,
            PictureBoxImageHolder imageHolder)
        {
            this.reader = reader;
            this.preprocessors = preprocessors;
            this.frequencyCounter = frequencyCounter;
            this.preprocessorSettings = preprocessorSettings;
            this.filePath = filePath;
            this.painter = painter;
            this.applicator = applicator;
            this.imageHolder = imageHolder;
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
            var preprocessed = preprocessors.Aggregate(words, (current, preprocessor) => preprocessor.Process(current));
            var infos = frequencyCounter.CountWordFrequencies(preprocessed);
            var wordInfos = applicator.GetWordsAndRectangles(infos);
            painter.Paint(applicator.WordsCenter, wordInfos);
            imageHolder.UpdateUi();
        }
    }
}