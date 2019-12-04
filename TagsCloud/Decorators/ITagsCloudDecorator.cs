using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouters;

namespace TagsCloud.Decorators
{
    public interface ITagsCloudDecorator
    {
        Font TagFont { get; set; }
        void CalcTagsRectanglesSizes(List<LayoutItem> layoutItems);
        void Render(List<LayoutItem> layoutItems, Image image);
    }
}
