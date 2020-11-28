using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagGenerator : ITagGenerator
    {
        private readonly ILayouter layouter;

        public TagGenerator(ILayouter layouter)
        {
            this.layouter = layouter;
        }

        public Dictionary<string, Rectangle> GetTags(Dictionary<string, int> wordEntry)
        {
            throw new NotImplementedException();
        }
    }
}