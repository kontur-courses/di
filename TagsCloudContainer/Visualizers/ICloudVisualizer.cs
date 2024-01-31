using SixLabors.ImageSharp;

namespace TagsCloudContainer.Visualizers;

public interface ICloudVisualizer
{
    public Image DrawImage(ITagCloud cloud);
}