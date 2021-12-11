using App.Implementation.SettingsHolders;

namespace GuiClient.UiActions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSizeSettings settings;

        public ImageSettingsAction(IImageHolder imageHolder, ImageSizeSettings settings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
            imageHolder.GenerateImage();
        }
    }
}