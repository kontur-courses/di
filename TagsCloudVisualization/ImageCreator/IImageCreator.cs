using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.ImageCreator
{
    public interface IImageCreator
    {
        Image Draw(IEnumerable<IDrawable> layout);
    }
}