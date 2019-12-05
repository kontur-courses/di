using System;
using System.Linq;
using System.Windows.Forms;
using TagCloud.WordsPreprocessing.DocumentParsers;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class OpenAction : IUIAction
    {
        private string[] allowedTypes;
        private ApplicationSettings applicationSettings;
        private Lazy<MainForm> mainForm;

        public OpenAction(ApplicationSettings settings, IDocumentParser[] parsers, Lazy<MainForm> mainForm)
        {
            this.mainForm = mainForm;
            applicationSettings = settings;
            allowedTypes = parsers.SelectMany(p => p.AllowedTypes).ToArray();
        }

        public string Category => "Файл";
        public string Name => "Открыть текстовый документ";
        public string Description => "Открывает текстовый документ на чтение";
        public void Perform()
        {
            var acceptedFormats = allowedTypes.Select(s => $"*{s}").Aggregate((a, b) => $"{a};{b}");
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = $"Текстовый документ|{acceptedFormats}"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                applicationSettings.FilePath = dialog.FileName;
                mainForm.Value.RedrawImage();
            }
        }
    }
}
