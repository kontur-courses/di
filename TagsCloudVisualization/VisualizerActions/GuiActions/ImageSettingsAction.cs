using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class ImageSettingsAction : IGuiAction
    {
        private readonly PictureBoxImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public ImageSettingsAction(PictureBoxImageHolder imageHolder,
            ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public string GetActionDescription()
        {
            return "Размеры изображения";
        }

        public string GetActionName()
        {
            return "Изображение...";
        }

        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
            imageHolder.SetImageSize(imageSettings);
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}