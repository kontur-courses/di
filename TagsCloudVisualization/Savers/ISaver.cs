using System.Drawing;

namespace TagsCloudVisualization.Savers
{
    public interface ISaver
    {
        public void SaveImage(Bitmap bitmap, string fileName);
    }
}