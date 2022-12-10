using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class ChoseBoringWordsSourceFileAction : IUiAction
    {
        private FileSettings fileSettings;

        public ChoseBoringWordsSourceFileAction(FileSettings fileSettings)
        {
            this.fileSettings = fileSettings;
        }

        public string Category => "Файл";
        public string Name => "Источник скучных слов...";
        public string Description => "Выбрать источник скучных слов для алгоритма";

        public void Perform()
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(fileSettings.CustomBoringWordsFilePath),
                DefaultExt = "txt",
                FileName = "boring.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                fileSettings.CustomBoringWordsFilePath = dialog.FileName;
        }
    }
}
