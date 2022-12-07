using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class SaveImageAction : IUiAction
    {
        private ImageHolder imageHolder;
        private FileSettings fileSettings;

        public SaveImageAction(ImageHolder imageHolder, FileSettings fileSettings)
        {
            this.imageHolder = imageHolder;
            this.fileSettings = fileSettings;
        }

        public string Category => "Файл";
        public string Name => "Сохранить как...";
        public string Description => "Сохранить файл как";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(fileSettings.ResultImagePath),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}
