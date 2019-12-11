using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouters;

namespace TagsCloud.Renderers
{
    public interface ITagsCloudRenderer
    {
        Font TagFont { get; set; }
        void CalcTagsRectanglesSizes(List<LayoutItem> layoutItems);
        void Render(List<LayoutItem> layoutItems, Image image);
    }
}
