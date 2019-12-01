using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step06.Infrastructure.Injection;
using FractalPainting.Solved.Step06.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step06.App.Actions
{
    public class ImageSettingsAction : IUiAction, INeed<IImageSettingsProvider>, INeed<IImageHolder>
    {
        private IImageHolder imageHolder;
        private IImageSettingsProvider imageSettingsProvider;

        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }

        public void SetDependency(IImageSettingsProvider dependency)
        {
            imageSettingsProvider = dependency;
        }

        public string Category => "Настройки";
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