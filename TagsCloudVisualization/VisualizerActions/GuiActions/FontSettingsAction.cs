using System.Drawing;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class FontSettingsAction : IGuiAction
    {
        private readonly AppSettings appSettings;

        public FontSettingsAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GetActionDescription()
        {
            return "Поменять шрифт";
        }

        public string GetActionName()
        {
            return "Шрифт...";
        }

        public void Perform()
        {
            SettingsForm.For(appSettings.Font).ShowDialog();
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