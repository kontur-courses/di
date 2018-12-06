using System.IO;
using System.Windows.Forms;

namespace TagsCloudContainer.UiActions.Actions
{
    public class LoadWordsAction : IUiAction
    {
        private readonly IFileReader reader;
        private readonly WordsPreprocessor preprocessor;
        private readonly WordsPreprocessorSettings preprocessorSettings;
        private readonly IFilePathProvider filePath;

        public LoadWordsAction(IFileReader reader, WordsPreprocessor preprocessor, WordsPreprocessorSettings preprocessorSettings, IFilePathProvider filePath)
        {
            this.reader = reader;
            this.preprocessor = preprocessor;
            this.preprocessorSettings = preprocessorSettings;
            this.filePath = filePath;
        }

        public MenuCategory Category => MenuCategory.TagCloud;
        public string Name => "Загрузить слова";
        public string Description => "Загрузить слова из файла";

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
            reader.Read();
            SettingsForm.For(preprocessorSettings).ShowDialog();
            preprocessor.CountWordFrequencies();
        }
    }
}