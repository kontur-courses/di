using System.Drawing;
using TagsCloudVisualization.DrawableContainers;

namespace TagsCloudVisualization.ImageCreators
{
    public interface IImageCreator
    {
        Image Draw(IDrawableContainer drawableContainer);
    }
}