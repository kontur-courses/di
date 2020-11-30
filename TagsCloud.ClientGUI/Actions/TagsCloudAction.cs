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

        public string Category => "Облако";
        public string Name => "Облако тегов";
        public string Description => "Да";

        public void Perform()
        {
            tagsCloudPainter.Paint();
        }
    }
}