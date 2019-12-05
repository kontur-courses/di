using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Painters;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class PaletteSettingsAction : IGuiAction
    {
        private readonly Palette palette;

        public PaletteSettingsAction(Palette palette)
        {
            this.palette = palette;
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
            SettingsForm.For(palette).ShowDialog();
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}