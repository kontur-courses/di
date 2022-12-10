using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private PictureBox pictureBox;
        private ImageSettings imageSettings;

        public ImageSettingsAction(PictureBox pictureBox, ImageSettings imageSettings)
        {
            this.pictureBox = pictureBox;
            this.imageSettings = imageSettings;
        }

        public string Category => "Изображение";
        public string Name => "Настройки...";
        public string Description => "Изменить параметры изоюражения";

        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
            if (imageSettings.Width > 0 && imageSettings.Height > 0)
            {
                pictureBox.RecreateImage(imageSettings);
                return;
            }

            MessageBox.Show("Только положительные значения!");
            Perform();
        }
    }
}
