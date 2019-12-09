using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Painters;
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
            if (appSettings.LastOpenedFile != null)
            {
                var newImage = TagCloudVisualizer.GetTagCloudFromFile(appSettings.LastOpenedFile);
                appSettings.ImageHolder.SetImage(newImage);
            }
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}