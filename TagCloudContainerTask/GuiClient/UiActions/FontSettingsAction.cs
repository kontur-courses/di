using App.Implementation.SettingsHolders;

namespace GuiClient.UiActions
{
    internal class FontSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly FontSettings settings;

        public FontSettingsAction(IImageHolder imageHolder, FontSettings settings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Шрифт...";
        public string Description => "Шрифты для тегов";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
            imageHolder.GenerateImage();
        }
    }
}