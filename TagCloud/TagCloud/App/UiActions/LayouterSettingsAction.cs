namespace TagCloud
{
    public class LayouterSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly LayouterSettings layouterSettings;

        public LayouterSettingsAction(IImageHolder imageHolder,
            LayouterSettings layouterSettings)
        {
            this.imageHolder = imageHolder;
            this.layouterSettings = layouterSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Layouter...";
        public string Description => "Layouter settings";

        public void Perform()
        {
            SettingsForm.For(layouterSettings).ShowDialog();
        }
    }
}
