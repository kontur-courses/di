using System.Drawing;

namespace TagsCloud.Tests.ImageFromTestSaver
{
    public interface IImageFromTestSaver
    {
        public bool TrySaveImageToFile(string testName, Image image, out string path);
    }
}