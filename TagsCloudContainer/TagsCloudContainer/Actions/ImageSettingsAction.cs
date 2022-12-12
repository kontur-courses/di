using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Services;

namespace TagsCloudContainer.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private ImageSettings imageSettings;
        private readonly ITagCloudService _tagCloudService;

        public ImageSettingsAction(ITagCloudService tagCloudService, PictureBox pictureBox, ImageSettings imageSettings)
        {
            this._tagCloudService = tagCloudService;
            this.imageSettings = imageSettings;
        }

        public string Category => "Изображение";
        public string Name => "Настройки...";
        public string Description => "Изменить параметры изоюражения";

        public void Perform()
        {
            _tagCloudService.SetSettings(imageSettings);
        }
    }
}
