﻿using TagCloudApp.Domain;

namespace TagCloudApp.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder _imageHolder;
        private readonly ImageSettings _imageSettings;

        public ImageSettingsAction(IImageHolder imageHolder,
            ImageSettings imageSettings)
        {
            _imageHolder = imageHolder;
            _imageSettings = imageSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            SettingsForm.For(_imageSettings).ShowDialog();
            _imageHolder.RecreateImage(_imageSettings);
        }
    }
}