using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class ImageSettingsAction : IGuiAction
    {
        private readonly AppSettings appSettings;

        public ImageSettingsAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
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
            SettingsForm.For(appSettings.ImageSettings).ShowDialog();
            appSettings.ImageHolder.SetImageSize(appSettings.ImageSettings);
            if (appSettings.CurrentFile != null)
            {
                var newImage = appSettings.CurrentInterface.GetTagCloud();
                appSettings.ImageHolder.SetImage(newImage);
            }
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}