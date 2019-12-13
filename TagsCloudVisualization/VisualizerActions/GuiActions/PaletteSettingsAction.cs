using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class PaletteSettingsAction : IGuiAction
    {
        private readonly AppSettings appSettings;

        public PaletteSettingsAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GetActionDescription()
        {
            return "Поменять палитру";
        }

        public string GetActionName()
        {
            return "Палитра...";
        }

        public void Perform()
        {
            SettingsForm.For(appSettings.Palette).ShowDialog();
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