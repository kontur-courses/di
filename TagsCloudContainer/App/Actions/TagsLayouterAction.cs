using System;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainer.App.Actions
{
    public class TagsLayouterAction : IUiAction
    {
        private TagsLayouter tagsLayouter;
        private ITagsPainter tagsPainter;

        public TagsLayouterAction(TagsLayouter tagsLayouter, ITagsPainter tagsPainter)
        {
            this.tagsLayouter = tagsLayouter;
            this.tagsPainter = tagsPainter;
        }

        public string Category => "Облако тегов";
        public string Name => "Облако тегов";
        public string Description => "Облако тегов";

        public void Perform()
        {
            var tags = tagsLayouter.PutAllTags();
            tagsPainter.Paint(tags);
        }
    }
}