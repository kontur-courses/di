using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly IImageSettingsProvider imageSettingsProvider;

        public ImageSettingsAction(IImageHolder imageHolder, IImageSettingsProvider imageSettingsProvider)
        {
            this.imageHolder = imageHolder;
            this.imageSettingsProvider = imageSettingsProvider;
        }

        #region IUiAction

        public string Category => "Настройки";
        public int CategoryOrder => 2;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            var imageSettings = imageSettingsProvider.ImageSettings;
            SettingsForm.For(imageSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }

        #endregion
    }
}