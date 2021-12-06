using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private IImageHolder imageHolder;
        private IImageSettingsProvider imageSettingsProvider;

        public ImageSettingsAction(IImageHolder imageHolder, IImageSettingsProvider imageSettingsProvider)
        {
            this.imageHolder = imageHolder;
            this.imageSettingsProvider = imageSettingsProvider;
        }
        

        public string CategoryName => "Настройки";
        public Category Category => Category.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            var imageSettings = imageSettingsProvider.ImageSettings;
            SettingsForm.For(imageSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}