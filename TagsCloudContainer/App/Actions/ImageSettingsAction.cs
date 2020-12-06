using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly AppSettings appSettings;

        public ImageSettingsAction(IImageHolder imageHolder, AppSettings appSettings)
        {
            this.imageHolder = imageHolder;
            this.appSettings = appSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            SettingsForm.For(appSettings.ImageSettings).ShowDialog();
            imageHolder.RecreateImage();
        }
    }
}