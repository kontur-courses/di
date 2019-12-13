using System.Collections.Generic;
using TagsCloud.Layouters;

namespace TagsCloud_Tests
{
    internal class TestingLayouter : ITagsCloudLayouter
    {
        public void ReallocItems(List<LayoutItem> items)
        {
            if (items.Count == 0) return;

            items.Sort((i1, i2) => i2.Rectangle.Square().CompareTo(i1.Rectangle.Square()));

            var biggestItem = items[0];
            biggestItem.Rectangle.X = 0;
            biggestItem.Rectangle.Y = 0;

            for (var i = 1; i < items.Count; i++)
            {
                items[i].Rectangle.Y = items[i - 1].Rectangle.Y + items[i - 1].Rectangle.Height;
            }
        }
    }
}
