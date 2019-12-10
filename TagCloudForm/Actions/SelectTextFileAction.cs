using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagCloud.TextProvider;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class SelectTextFileAction : IUiAction
    {
        private IImageDirectoryProvider imageDirectoryProvider;
        private TextFileReader textFileReader;
        private CloudPainter cloudPainter;

        public SelectTextFileAction(IImageDirectoryProvider imageDirectoryProvider, TextFileReader textFileReader,
            CloudPainter cloudPainter)
        {
            this.imageDirectoryProvider = imageDirectoryProvider;
            this.textFileReader = textFileReader;
            this.cloudPainter = cloudPainter;
        }

        public string Category => "Текст";
        public string Name => "Выбрать файл";
        public string Description => "Выбрать файл с текстом";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                DefaultExt = "txt",
                Multiselect = true
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                textFileReader.FilesPaths = new HashSet<string>(dialog.FileNames);
                cloudPainter.ResetWordsFrequenciesDictionary();
                cloudPainter.Paint();
            }
        }
    }
}