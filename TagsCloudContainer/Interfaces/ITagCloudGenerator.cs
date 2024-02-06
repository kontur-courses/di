using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ITagCloudGenerator
    {
        Bitmap GenerateTagCloud(IEnumerable<string> words, IImageSettings imageSettings);
    }
}
