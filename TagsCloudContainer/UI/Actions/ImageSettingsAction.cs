using System.Windows.Forms;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.UI.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public ImageSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            var res = SettingsForm.For(imageSettings).ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.RecreateImage(imageSettings);
        }
    }
}