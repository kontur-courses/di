using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class ChoseSourceFileAction : IUiAction
    {
        private FileSettings fileSettings;

        public ChoseSourceFileAction(FileSettings fileSettings)
        {
            this.fileSettings = fileSettings;
        }

        public string Category => "Файл";
        public string Name => "Источник данных...";
        public string Description => "Выбрать источник данных для алгоритма";

        public void Perform()
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(fileSettings.SourceFilePath),
                DefaultExt = "txt",
                FileName = "source.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                fileSettings.SourceFilePath = dialog.FileName;
        }
    }
}
