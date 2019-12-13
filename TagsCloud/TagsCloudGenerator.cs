using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Layouters;
using TagsCloud.Renderers;

namespace TagsCloud
{
    public class TagsCloudGenerator
    {
        private readonly ITagsCloudLayouter layouter;
        private readonly ITagsCloudRenderer renderer;

        public TagsCloudGenerator(ITagsCloudLayouter layouter, ITagsCloudRenderer renderer)
        {
            this.layouter = layouter;
            this.renderer = renderer;
        }

        public Image GenerateCloud(List<string> words)
        {
            var tags = DetermineTags(words);
            var layout = GetLayout(tags);
            TagCloudImage = renderer.Render(layout);
            return TagCloudImage;
        }

        private List<(string Tag, int Rate)> DetermineTags(List<string> words)
        {
            var tags = new List<(string Tag, int Rate)>();
            foreach (var group in words.GroupBy(word => word))
                tags.Add((group.Key, group.Count()));
            return tags;
        }

        private List<LayoutItem> GetLayout(List<(string Tag, int Rate)> tags)
        {
            var layoutItems = tags.ConvertAll(t => new LayoutItem(new Rectangle(Point.Empty, Size.Empty), t.Tag, t.Rate));
            renderer.CalcTagsRectanglesSizes(layoutItems);
            layouter.ReallocItems(layoutItems);
            return layoutItems;
        }

        public Image TagCloudImage { get; private set; }
    }
}
