using TagCloud.TagCloudVisualizations;
using TagCloudGui.Infrastructure;
using TagCloudGui.Infrastructure.Common;

namespace TagCloudGui.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ITagCloudVisualizationSettings settings;

        public ImageSettingsAction(IImageHolder imageHolder,
            ITagCloudVisualizationSettings settings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Настройки";
        public string Description => "Настройки облака";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}
