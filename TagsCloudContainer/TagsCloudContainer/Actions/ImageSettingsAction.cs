using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private ImageHolder imageHolder;
        private ImageSettings imageSettings;

        public ImageSettingsAction(ImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public string Category => "Изображение";
        public string Name => "Настройки...";
        public string Description => "Изменить параметры изоюражения";

        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}
