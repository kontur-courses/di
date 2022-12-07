using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder _imageHolder;
        private readonly IImageSettingsProvider _imageSettingsProvider;

        public ImageSettingsAction(
            IImageHolder imageHolder,
            IImageSettingsProvider imageSettingsProvider
        )
        {
            _imageHolder = imageHolder;
            _imageSettingsProvider = imageSettingsProvider;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Image...";
        public string Description => "Image size";

        public void Perform()
        {
            SettingsForm.For(_imageSettingsProvider.GetImageSettings()).ShowDialog();
            _imageHolder.RecreateImage();
        }
    }
}