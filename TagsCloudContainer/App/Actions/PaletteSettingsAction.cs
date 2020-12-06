using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly AppSettings appSettings;

        public PaletteSettingsAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Палитра...";
        public string Description => "Цвета для облака тегов";

        public void Perform()
        {
            SettingsForm.For(appSettings.Palette).ShowDialog();
        }
    }
}