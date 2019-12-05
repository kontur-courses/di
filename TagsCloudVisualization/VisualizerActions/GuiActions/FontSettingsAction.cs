using System.Drawing;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Painters;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class FontSettingsAction : IGuiAction
    {
        private readonly Font font;

        public FontSettingsAction(Font font)
        {
            this.font = font;
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
            SettingsForm.For(font).ShowDialog();
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}