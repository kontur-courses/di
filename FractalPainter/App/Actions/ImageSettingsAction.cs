using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class ImageSettingsAction : IUiAction//, INeed<IImageSettingsProvider>, INeed<IImageHolder>
    {
        private IImageHolder imageHolder;
        private ImageSettings imageSettings;
        //private IImageSettingsProvider imageSettingsProvider;

        public ImageSettingsAction(IImageHolder dependency, ImageSettings imageSettings)
        {
            imageHolder = dependency;
            this.imageSettings = imageSettings;
        }
        /*
        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }
        */
        /*
        public void SetDependency(IImageSettingsProvider dependency)
        {
            imageSettingsProvider = dependency;
        }
        */

        public string Category => "Настройки";
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            //var imageSettings = imageSettingsProvider.ImageSettings;
            SettingsForm.For(imageSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}