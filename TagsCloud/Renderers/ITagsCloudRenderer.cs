using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouters;

namespace TagsCloud.Renderers
{
    public interface ITagsCloudRenderer
    {
        Font TagFont { get; set; }
        int ImageWidth { get; set; }
        int ImageHeight { get; set; }
        void CalcTagsRectanglesSizes(List<LayoutItem> layoutItems);
        Image Render(List<LayoutItem> layoutItems);
    }
}
