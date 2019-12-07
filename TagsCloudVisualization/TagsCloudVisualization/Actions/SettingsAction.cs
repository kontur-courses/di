namespace TagsCloudVisualization.Actions
{
    public class SettingsAction : IUiAction
    {
        private readonly ImageSettingsProvider imageSettingsProvider;
        public string Name { get; }

        public SettingsAction(ImageSettingsProvider imageSettingsProvider)
        {
            Name = "Settings";
            this.imageSettingsProvider = imageSettingsProvider;
        }

        public void Perform()
        {
            var settings = new SettingsForm(imageSettingsProvider.ImageSettings);
            settings.ShowDialog();
        }
    }
}