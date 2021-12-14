using System.Drawing;

namespace TagsCloudVisualization.Common.ImageWriters
{
    public interface IImageWriter
    {
        public void Save(Bitmap bitmap, string path);
    }
}