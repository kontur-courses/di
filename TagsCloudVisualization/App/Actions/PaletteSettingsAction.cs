using TagsCloudVisualization.TagsCloud;

namespace TagsCloudVisualization.App.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        public string Name => "Палитра";
        public string Category => "Настройки";
        private readonly Palette palette;

        public PaletteSettingsAction(Palette palette)
        {
            this.palette = palette;
        }
        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}