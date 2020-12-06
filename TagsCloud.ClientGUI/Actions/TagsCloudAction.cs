using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI.Actions
{
    public class TagsCloudAction : IUiAction
    {
        private readonly ITagsCloudFactory cloudFactory;
        private readonly TagsCloudPainter tagsCloudPainter;

        public TagsCloudAction(TagsCloudPainter tagsCloudPainter, ITagsCloudFactory cloudFactory)
        {
            this.tagsCloudPainter = tagsCloudPainter;
            this.cloudFactory = cloudFactory;
        }

        public string Category => "Облако тегов";
        public string Name => "Нарисовать облако тегов";
        public string Description => "Рисование облака тегов по спирали";

        public void Perform()
        {
            tagsCloudPainter.Paint(cloudFactory.Create());
        }
    }
}