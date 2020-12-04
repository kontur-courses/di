using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class FontSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public FontSettingsAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Шрифт...";
        public string Description => "Шрифты для тегов";

        public void Perform()
        {
            SettingsForm.For(imageHolder.GetAppSettings().FontSettings).ShowDialog();
        }
    }
}