using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.FormAction
{
    public class PaletteAction : IFormAction
    {
        public string Category => "Settings";
        public string Name => "Colors";
        public string Description => "Change colors of your cloud visualization";
        
        private readonly Palette palette;
        
        public PaletteAction(Palette palette)
        {
            this.palette = palette;
        }
        
        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}