namespace TagsCloudContainer.UiActions.Actions
{
    public class CloudDrawAction : IUiAction
    {
        private readonly TagCloudPainter painter;

        public CloudDrawAction(TagCloudPainter painter)
        {
            this.painter = painter;
        }

        public MenuCategory Category => MenuCategory.TagCloud;
        public string Name => "Нарисовать облако";
        public string Description => "Нарисовать облако тегов";

        public void Perform()
        {
            painter.Paint();
        }
    }
}