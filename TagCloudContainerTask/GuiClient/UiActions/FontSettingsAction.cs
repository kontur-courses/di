using App.Implementation.SettingsHolders;

namespace GuiClient.UiActions
{
    internal class FontSettingsAction : IUiAction
    {
        private readonly FontSettings settings;

        public FontSettingsAction(FontSettings settings)
        {
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Шрифт...";
        public string Description => "Шрифты для тегов";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}