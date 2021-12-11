using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.ImageCreators
{
    public interface IImageCreator
    {
        Image Draw(IEnumerable<IDrawable> layout);
    }
}