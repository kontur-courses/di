using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly IPaletteSettings settings;

        public PaletteSettingsAction(IPaletteSettings settings)
        {
            this.settings = settings;
        }

        public Category Category => Category.Settings;
        public string Name => "палитра...";
        public string Description => "";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}