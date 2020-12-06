using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class FontSettingsAction : IUiAction
    {
        private readonly AppSettings appSettings;

        public FontSettingsAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Шрифт...";
        public string Description => "Шрифты для тегов";

        public void Perform()
        {
            SettingsForm.For(appSettings.FontSettings).ShowDialog();
        }
    }
}