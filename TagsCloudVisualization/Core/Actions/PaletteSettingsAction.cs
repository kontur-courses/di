using TagsCloudGenerator.Infrastructure;
using TagsCloudVisualization.Core.Painter;
using TagsCloudVisualization.Infrastructure.Common;
using TagsCloudVisualization.Infrastructure.UiActions;

namespace TagsCloudVisualization.Core.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly Palette palette;
        private readonly TagCloudPainter.Factory tagCloudPainterFactory;
        private readonly TagCloudSettings tagCloudSettings;

        public PaletteSettingsAction(Palette palette,
            TagCloudSettings tagCloudSettings,
            TagCloudPainter.Factory tagCloudPainterFactory)
        {
            this.palette = palette;
            this.tagCloudSettings = tagCloudSettings;
            this.tagCloudPainterFactory = tagCloudPainterFactory;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Палитра...";
        public string Description => "Цвета";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
            tagCloudPainterFactory(tagCloudSettings).Paint();
        }
    }
}