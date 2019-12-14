using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class ImageSettingsSetGuiAction : ImageSettingsSetAction, IGuiAction
    {
        public ImageSettingsSetGuiAction(AppSettings appSettings) : base(appSettings)
        {}

        public override string GetActionDescription()
        {
            return "Размеры изображения";
        }

        public override string GetActionName()
        {
            return "Изображение...";
        }

        protected override ImageSettings GetImageSettings()
        {
            var imageSettings = appSettings.ImageSettings;
            SettingsForm.For(imageSettings).ShowDialog();
            return imageSettings;
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}