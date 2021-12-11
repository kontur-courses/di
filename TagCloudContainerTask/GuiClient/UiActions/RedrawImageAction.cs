namespace GuiClient.UiActions
{
    public class RedrawImageAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public RedrawImageAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public MenuCategory Category => MenuCategory.Redraw;

        public string Name => "Нарисовать";

        public string Description => "Обновить изображение";

        public void Perform()
        {
            imageHolder.GenerateImage();
        }
    }
}