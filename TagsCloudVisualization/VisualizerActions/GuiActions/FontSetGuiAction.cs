using System.Drawing;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class FontSetGuiAction : FontSetAction, IGuiAction
    {
        public FontSetGuiAction(AppSettings appSettings) : base(appSettings)
        {}

        public override string GetActionDescription()
        {
            return "Поменять шрифт";
        }

        public override string GetActionName()
        {
            return "Шрифт...";
        }

        protected override Font GetFont()
        {
            var font = appSettings.Font;
            SettingsForm.For(font).ShowDialog();
            return font;
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}