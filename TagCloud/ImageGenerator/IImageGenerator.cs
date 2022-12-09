using System.Drawing;

namespace TagCloud.ImageGenerator;

public interface IImageGenerator
{
    public Image GenerateImage(Tag[] tags);
}