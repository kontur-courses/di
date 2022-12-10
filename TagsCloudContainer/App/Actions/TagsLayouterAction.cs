using System;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainer.App.Actions
{
    public class TagsLayouterAction : IUiAction
    {
        private TagsLayouter tagsLayouter;

        public TagsLayouterAction(TagsLayouter tagsLayouter)
        {
            this.tagsLayouter = tagsLayouter;
        }

        public string Category => "Облако тегов";
        public string Name => "Облако тегов";
        public string Description => "Облако тегов";

        public void Perform()
        {
            tagsLayouter.PutAllTags();
            tagsLayouter.Paint();
        }
    }
}