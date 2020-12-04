using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public PaletteSettingsAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Палитра...";
        public string Description => "Цвета для облака тегов";

        public void Perform()
        {
            SettingsForm.For(imageHolder.GetAppSettings().Palette).ShowDialog();
        }
    }
}