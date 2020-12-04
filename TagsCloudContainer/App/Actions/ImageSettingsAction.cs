using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public ImageSettingsAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            SettingsForm.For(imageHolder.GetAppSettings().ImageSettings).ShowDialog();
            imageHolder.RecreateImage(imageHolder.GetAppSettings());
        }
    }
}