using App.Implementation.SettingsHolders;

namespace GuiClient.UiActions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly PaletteSettings palette;

        public PaletteSettingsAction(IImageHolder imageHolder, PaletteSettings palette)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Палитра...";
        public string Description => "Цвета для облака тегов";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
            imageHolder.GenerateImage();
        }
    }
}