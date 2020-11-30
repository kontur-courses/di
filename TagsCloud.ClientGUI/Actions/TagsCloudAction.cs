using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI.Actions
{
    public class TagsCloudAction : IUiAction
    {
        private readonly TagsCloudPainter tagsCloudPainter;

        public TagsCloudAction(TagsCloudPainter tagsCloudPainter)
        {
            this.tagsCloudPainter = tagsCloudPainter;
        }

        public string Category => "Облако тегов";
        public string Name => "Нарисовать облако тегов";
        public string Description => "Рисование облака тегов по спирали";

        public void Perform()
        {
            tagsCloudPainter.Paint();
        }
    }
}