using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ISaver
    {
        public void SaveImage(Bitmap bitmap, string fileName);
    }
}
