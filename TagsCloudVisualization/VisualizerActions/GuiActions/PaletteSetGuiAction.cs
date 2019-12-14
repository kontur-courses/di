using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class PaletteSetGuiAction : PaletteSetAction, IGuiAction
    {
        public PaletteSetGuiAction(AppSettings appSettings) : base(appSettings)
        {}

        public override string GetActionDescription()
        {
            return "Поменять палитру";
        }

        public override string GetActionName()
        {
            return "Палитра...";
        }

        protected override Palette GetPalette()
        {
            var palette = appSettings.Palette;
            SettingsForm.For(palette).ShowDialog();
            return palette;
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}