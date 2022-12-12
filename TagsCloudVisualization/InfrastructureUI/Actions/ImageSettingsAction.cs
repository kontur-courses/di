using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings settings;

        public ImageSettingsAction(IImageHolder imageHolder, ImageSettings settings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
        }

        public Category Category => Category.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}