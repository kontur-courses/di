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
        private ImageSettings settings;

        public ImageSettingsAction(ImageSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Изображение";
        public string Name => "Настройки...";
        public string Description => "Изменить параметры изоюражения";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}
