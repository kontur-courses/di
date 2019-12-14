using System;
using System.Linq;
using System.Windows.Forms;
using TagCloud.WordsPreprocessing.DocumentParsers;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class OpenAction : IUiAction
    {
        private readonly string[] allowedTypes;
        private readonly ApplicationSettings applicationSettings;
        private CloudConfigurationAction cloudConfigurationAction;

        public OpenAction(ApplicationSettings settings, IDocumentParser[] parsers, CloudConfigurationAction cloudConfigurationAction)
        {
            this.cloudConfigurationAction = cloudConfigurationAction;
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
                cloudConfigurationAction.Perform();
            }
        }
    }
}
