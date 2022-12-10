using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class SaveImageAction : IUiAction
    {
        private PictureBox pictureBox;
        private FileSettings fileSettings;

        public SaveImageAction(PictureBox pictureBox, FileSettings fileSettings)
        {
            this.pictureBox = pictureBox;
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
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png|Изображения (*.jpg)|*.jpg|Изображения (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                pictureBox.SaveImage(dialog.FileName);
        }
    }
}
